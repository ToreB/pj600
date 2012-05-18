using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.OleDb;
using System.Data;

using log4net;
using no.nith.pj600.dashboard.Code;
using System.Web.UI.DataVisualization.Charting;

namespace no.nith.pj600.dashboard
{
   public partial class _Default : System.Web.UI.Page
   {
      private static readonly ILog log = LogManager.GetLogger(typeof(_Default));
      private DatabaseClassesDataContext dataContext;
      private double hourPrice;

      private const string ASC = "asc";
      private const string DESC = "desc";

      //DataTable columns
      private const string PROJECT_NO = "ProjectNo";
      private const string PROJECT_NAME = "ProjectName";
      private const string CUSTOMER_NAME = "CustomerName";
      private const string PROJECT_MANAGER = "ProjectManager";
      private const string PROJECT_START_TIME = "ProjectStartTime";
      private const string PROJECT_STOP_TIME = "ProjectStopTime";
      private const string PROJECT_HOUR_ESTIMATE = "ProjectHourEstimate";
      private const string PROJECT_COST_ESTIMATE = "ProjectCostEstimate";
      private const string HOURS_SPENT = "HoursSpent";
      private const string TOTAL_SALES_AMOUNT = "TotalSalesAmount";
      private const string BALANCE_AMOUNT = "BalanceAmount";
      private const string ARTICLE_NAME = "ArticleName";
      private const string ARTICLE_NO = "ArticleNo";

      protected void Page_Load(object sender, EventArgs e)
      {
         //Loads the Overview tab when the page loads, if it's not a postback.
         if (!Page.IsPostBack)
         {
            Reset();
            LoadActiveTab("", "");
         }
         else //if postback
         {
            
         }
      }

      /*
       * Method that's called when the RowCreated event is raised in the GridView Server Control,
       * when a row is created.
       * Adds a sorting image to the row that the GridView is beeing sorted on.
       */
      protected void RowCreated(object sender, GridViewRowEventArgs e)
      {
         GridView gridView = (GridView)sender;

         //If the row is a header
         if (e.Row.RowType == DataControlRowType.Header)
         {
            foreach (TableCell cell in e.Row.Cells)
            {
               if (cell.HasControls())
               {
                  LinkButton button = cell.Controls[0] as LinkButton;

                  if (button != null)
                  {
                     //Checks if the LinkButton's text is equal the SortExpression
                     if (button.Text.Equals(MapSortExpressionToHeaderText()))
                     {
                        AddSortImage(cell);
                     }  
                  }
               }
            }
         }
      }

      /**
       * <summary>
       * Adds a sorting image to a cell, if the GridView is beeing sorted.
       * </summary>
       */
      private void AddSortImage(TableCell cell)
      {
         Image sortImage = new Image();

         if (SortExpression != "")
         {
            if (ViewState["sortOrder"].Equals(ASC))
            {
               sortImage.ImageUrl = "~/Images/arrow_sort_asc.png";
               sortImage.AlternateText = "asc";
            }
            else
            {
               sortImage.ImageUrl = "~/Images/arrow_sort_desc.png";
               sortImage.AlternateText = "desc";
            }
            
            cell.Controls.Add(sortImage);
         }
      }

      /*
       * Method that's called when the PageIndexChanging event is called from a GridView Server Control,
       * when one of the pager buttons is clicked.
       * Changes the PageIndex to the new page and reloads the active tab.
       */
      protected void OnPageIndexChanging(object sender, EventArgs e)
      {
         ((GridView)sender).PageIndex = ((GridViewPageEventArgs)e).NewPageIndex;
         LoadActiveTab(SortExpression, ViewState["sortOrder"].ToString());
      }

      /*
       * Method that's called when the TabChanged event is raised from the TabContainer Server Control,
       * when a tab is changed after a postback.
       * Resets the ViewState and loads the new tab.
       */
      protected void TabContainerTabChange(object sender, EventArgs e)
      {
         Reset();
         LoadActiveTab("", "");
      }

      /**
       * <summary>
       * Loads the active TabPanel in the TabContainer, with the specified sorting expression and sorting order.
       * </summary>
       */
      private void LoadActiveTab(string sortExpression, string sortOrder)
      {
         if (TabContainer.ActiveTab.ID.Equals("OverviewTab"))
         {
            LoadOverviewTab(sortExpression, sortOrder);
         }
         else if (TabContainer.ActiveTab.ID.Equals("SLATab"))
         {
            LoadSLATab(sortExpression, sortOrder);
         }
         else if (TabContainer.ActiveTab.ID.Equals("AddlServicesTab"))
         {
            LoadAddlServicesTab(sortExpression, sortOrder);
         }
         else
         {
            LoadGraphsTab();
         }
      }

      /**
       * <summary>
       * Loads the OverviewTab, with the specified sorting expression and sorting order.
       * </summary>
       */
      private void LoadOverviewTab(string sortExpression, string sortOrder)
      {
         dataContext = new DatabaseClassesDataContext();

         //Gets the HourPrice configured in the Web.Config's AppSettings section
         hourPrice = Convert.ToDouble(ConfigurationManager.AppSettings["HourPrice"]);

         //Query that gets the project number, project name, customer name, project manager, project start time,
         //project stop time, hours spent, sum of total sales amount and the latest balance for each SLA project.
         var query = (from project in dataContext.Projects
                      join slaProjects in dataContext.SLAProjects on project.ProjectNo equals slaProjects.ProjectNo
                      join customer in dataContext.Customers on project.CustomerNo equals customer.CustomerNo
                      join employee in dataContext.Employees on project.PMEmployeeNo equals employee.EmployeeNo
                      join tripletexImport in
                         (
                            from ti in dataContext.TripletexImports
                            group ti by ti.ProjectNo into g
                            select new { ProjectNo = g.Key, HoursSpent = g.Sum(p => p.Hours) }
                            ) on project.ProjectNo equals tripletexImport.ProjectNo into tripletexImportGroup
                      from hoursSpent in tripletexImportGroup.DefaultIfEmpty()
                      join salesFigures in
                         (
                            from sf in dataContext.SalesFigures
                            group sf by sf.ProjectNo into g
                            select new { ProjectNo = g.Key, TotalSalesAmount = g.Sum(p => p.TotalSalesAmount) }
                         ) on project.ProjectNo equals salesFigures.ProjectNo into salesFiguresGroup
                      from salesFigures in salesFiguresGroup.DefaultIfEmpty()
                      join balance in
                         (
                            from b in dataContext.Balances
                            orderby b.LastUpdate descending, b.Year descending, b.Period descending
                            select new { ProjectNo = b.ProjectNo, BalanceAmount = b.Amount }
                         ) on project.ProjectNo equals balance.ProjectNo into balanceGroup
                      from balance in balanceGroup.DefaultIfEmpty().Take(1)
                      select new 
                      {
                         ProjectNo = project.ProjectNo,
                         ProjectName = project.Name,
                         CustomerName = customer.Name,
                         ProjectManager = employee.Name,
                         ProjectStartTime = project.StartTime,
                         ProjectStopTime = project.StopTime,
                         HoursSpent = hoursSpent.HoursSpent != null ? hoursSpent.HoursSpent * hourPrice : 0.0,
                         TotalSalesAmount = salesFigures.TotalSalesAmount != null ? salesFigures.TotalSalesAmount : 0.0,
                         BalanceAmount = balance.BalanceAmount != null ? balance.BalanceAmount : 0.0
                      }).Distinct();

         //Creates a DataTable to fill with the results from the query.
         DataTable dt = new DataTable();
         dt.Columns.Add(PROJECT_NO, Type.GetType("System.Int32"));
         dt.Columns.Add(PROJECT_NAME);
         dt.Columns.Add(CUSTOMER_NAME);
         dt.Columns.Add(PROJECT_MANAGER);
         dt.Columns.Add(PROJECT_START_TIME, Type.GetType("System.DateTime"));
         dt.Columns.Add(PROJECT_STOP_TIME, Type.GetType("System.DateTime"));
         dt.Columns.Add(HOURS_SPENT, Type.GetType("System.Double"));
         dt.Columns.Add(TOTAL_SALES_AMOUNT, Type.GetType("System.Double"));
         dt.Columns.Add(BALANCE_AMOUNT, Type.GetType("System.Double"));

         //Fills the DataTable with data
         foreach (var row in query)
         {
            DataRow newRow = dt.NewRow();

            newRow[PROJECT_NO] = row.ProjectNo;
            newRow[PROJECT_NAME] = row.ProjectName;
            newRow[CUSTOMER_NAME] = row.CustomerName;
            newRow[PROJECT_MANAGER] = row.ProjectManager;
            newRow[PROJECT_START_TIME] = row.ProjectStartTime;
            newRow[PROJECT_STOP_TIME] = row.ProjectStopTime;
            newRow[HOURS_SPENT] = row.HoursSpent;
            newRow[TOTAL_SALES_AMOUNT] = row.TotalSalesAmount;
            newRow[BALANCE_AMOUNT] = row.BalanceAmount;

            dt.Rows.Add(newRow);
         }

         //Binds the data to the GridView
         OverviewTable.DataSource = dt;
         OverviewTable.DataBind();

         //Sorts the GridView
         if (sortExpression != string.Empty)
         {
            Sort(OverviewTable, sortExpression, sortOrder);
         }
         else
         {
            Sort(OverviewTable, "ProjectNo", ASC);
         }
      }

      /**
       * <summary>
       * Loads the SLATab, with the specified sorting expression and sorting order.
       * </summary>
       */
      private void LoadSLATab(string sortExpression, string sortOrder)
      {
         dataContext = new DatabaseClassesDataContext();

         //Query that gets the project number, project name, customer name, project manager
         //and the latest balance for each SLA project.
         var mainQuery = (from project in dataContext.Projects
                          join customer in dataContext.Customers on project.CustomerNo equals customer.CustomerNo
                          join slaProjects in dataContext.SLAProjects on project.ProjectNo equals slaProjects.ProjectNo
                          join employee in dataContext.Employees on project.PMEmployeeNo equals employee.EmployeeNo
                          join balance in
                             (
                                from b in dataContext.Balances
                                orderby b.LastUpdate descending, b.Year descending, b.Period descending
                                select new { ProjectNo = b.ProjectNo, BalanceAmount = b.Amount }
                             ) on project.ProjectNo equals balance.ProjectNo into balanceGroup
                          from balance in balanceGroup.DefaultIfEmpty().Take(1)
                          orderby project.ProjectNo ascending
                          select new
                          {
                             ProjectNo = project.ProjectNo,
                             ProjectName = project.Name,
                             CustomerName = customer.Name,
                             ProjectManager = employee.Name,
                             BalanceAmount = balance.BalanceAmount != null ? balance.BalanceAmount : 0
                          }).Distinct();

         //Creates a DataTable to fill with the results from the query.
         DataTable dt = new DataTable();
         dt.Columns.Add(PROJECT_NO, Type.GetType("System.Int32"));
         dt.Columns.Add(PROJECT_NAME);
         dt.Columns.Add(CUSTOMER_NAME);
         dt.Columns.Add(PROJECT_MANAGER);
         dt.Columns.Add(BALANCE_AMOUNT, Type.GetType("System.Double"));

         //Fills the DataTable with data
         foreach (var row in mainQuery)
         {
            DataRow newRow = dt.NewRow();

            newRow[PROJECT_NO] = row.ProjectNo;
            newRow[PROJECT_NAME] = row.ProjectName;
            newRow[CUSTOMER_NAME] = row.CustomerName;
            newRow[PROJECT_MANAGER] = row.ProjectManager;
            newRow[BALANCE_AMOUNT] = row.BalanceAmount;

            dt.Rows.Add(newRow);
         }

         //Binds the data to the GridView
         SLATable.DataSource = dt;
         SLATable.DataBind();

         //Sorts the GridView
         if (sortExpression != string.Empty)
         {
            Sort(SLATable, sortExpression, sortOrder);
         }
         else
         {
            Sort(SLATable, "ProjectNo", ASC);
         }
      }

      /**
       * <summary>
       * Loads the AddlServicesTab, with the specified sorting expression and sorting order.
       * </summary>
       */
      private void LoadAddlServicesTab(string sortExpression, string sortOrder)
      {
         dataContext = new DatabaseClassesDataContext();

         //Query that gets the project number, project name, customer name and the sum of total sales amount for a sla project,
         //and also gets the article numbers and article names of the articles sold to the customer.
         var query = (from project in dataContext.Projects
                      join customer in dataContext.Customers on project.CustomerNo equals customer.CustomerNo
                      join slaProjects in dataContext.SLAProjects on project.ProjectNo equals slaProjects.ProjectNo
                      join article in
                         (
                           from a in dataContext.Articles
                            join sf in dataContext.SalesFigures on a.ArticleNo equals sf.ArticleNo
                            select new { ArticleNo = a.ArticleNo, ArticleName = a.Name, ProjectNo = sf.ProjectNo }
                         ) on project.ProjectNo equals article.ProjectNo into articelSelection
                      from article in articelSelection.Distinct().DefaultIfEmpty()
                      join salesFigures in
                         (
                           from sf in dataContext.SalesFigures
                           group sf by new { sf.ProjectNo, sf.ArticleNo } into g
                           select new
                           {
                              ProjectNo = g.Key.ProjectNo,
                              ArticleNo = g.Key.ArticleNo,
                              TotalSalesAmount = g.Sum(p => p.TotalSalesAmount)
                           }
                         ) on new { project.ProjectNo, article.ArticleNo } equals new { salesFigures.ProjectNo, salesFigures.ArticleNo }
                      select new 
                      {
                        ProjectNo = project.ProjectNo,
                        ProjectName = project.Name,
                        CustomerName = customer.Name,
                        ArticleNo = article.ArticleNo,
                        ArticleName = article.ArticleName,
                        TotalSalesAmount = salesFigures.TotalSalesAmount
                      }).Distinct();

         //Creates a DataTable to fill with the results from the query.
         DataTable dt = new DataTable();
         dt.Columns.Add(PROJECT_NO, Type.GetType("System.Int32"));
         dt.Columns.Add(PROJECT_NAME);
         dt.Columns.Add(CUSTOMER_NAME);
         dt.Columns.Add(ARTICLE_NO);
         dt.Columns.Add(ARTICLE_NAME);
         dt.Columns.Add(TOTAL_SALES_AMOUNT, Type.GetType("System.Double"));

         //Fills the DataTable with data.
         foreach (var row in query)
         {
            DataRow newRow = dt.NewRow();

            newRow[PROJECT_NO] = row.ProjectNo;
            newRow[PROJECT_NAME] = row.ProjectName;
            newRow[CUSTOMER_NAME] = row.CustomerName;
            newRow[ARTICLE_NO] = row.ArticleNo;
            newRow[ARTICLE_NAME] = row.ArticleName;
            newRow[TOTAL_SALES_AMOUNT] = row.TotalSalesAmount;

            dt.Rows.Add(newRow);
         }

         //Binds the data to the GridView
         AddlServicesTable.DataSource = dt;
         AddlServicesTable.DataBind();

         //Sorts the GridView
         if (sortExpression != string.Empty)
         {
            Sort(AddlServicesTable, sortExpression, sortOrder);
         }
         else
         {
            Sort(AddlServicesTable, "ProjectNo", ASC);
         }
      }

      /**
       * <summary>
       * Loads the GraphsTab.
       * </summary>
       */
      private void LoadGraphsTab()
      {
         //Fills the DataSelect DropDownList with data
         string[] columns = {BALANCE_AMOUNT, HOURS_SPENT, TOTAL_SALES_AMOUNT };
         DataSelect.DataSource = columns;
         DataSelect.DataBind();

         //Fills the TypeSelect DropDownList with data
         SeriesChartType[] types = { SeriesChartType.Column, SeriesChartType.Pie };
         TypeSelect.DataSource = types;
         TypeSelect.DataBind();

         //Fills the CountSelect DropDownList with data
         int[] counts = { 5, 10, 15, 20 };
         CountSelect.DataSource = counts;
         CountSelect.DataBind();

         //Creates the initial graph
         CreateGraph(BALANCE_AMOUNT, SeriesChartType.Column, 5, DESC);
      }
    
      /**
       * <summary>
       * Method that creates a graph with the specified data from the dataSelection parameter, as the type specified 
       * by the chartType parameter, with the specified number of projects from the count paramater and in the direction
       * specified by the direction parameter.
       * </summary>
       */
      private void CreateGraph(string dataSelection, SeriesChartType chartType, int count, string direction)
      {
         dataContext = new DatabaseClassesDataContext();

         //Gets the HourPrice configured in the Web.Config's AppSettings section
         hourPrice = Convert.ToDouble(ConfigurationManager.AppSettings["HourPrice"]);

         //Query that gets the Project name, customer name and the latest balance for each SLA project
         var balanceAmountQuery = (from project in dataContext.Projects
                                   join slaProjects in dataContext.SLAProjects on project.ProjectNo equals slaProjects.ProjectNo
                                   join customer in dataContext.Customers on project.CustomerNo equals customer.CustomerNo
                                   join balance in
                                      (
                                         from b in dataContext.Balances
                                         orderby b.LastUpdate descending, b.Year descending, b.Period descending
                                         select new { ProjectNo = b.ProjectNo, BalanceAmount = b.Amount }
                                      ) on project.ProjectNo equals balance.ProjectNo into balanceGroup
                                   from balance in balanceGroup.DefaultIfEmpty().Take(1)
                                   select new
                                   {
                                      ProjectName = project.Name,
                                      CustomerName = customer.Name,
                                      BalanceAmount = balance.BalanceAmount != null ? balance.BalanceAmount : 0.0
                                   }).Distinct();

         //Query that gets the project name, customer name and the product of the sum of the hours spent
         //multiplied with the hour price
         var hoursSpentQuery = (from project in dataContext.Projects
                                join slaProjects in dataContext.SLAProjects on project.ProjectNo equals slaProjects.ProjectNo
                                join customer in dataContext.Customers on project.CustomerNo equals customer.CustomerNo
                                join tripletexImport in
                                   (
                                      from ti in dataContext.TripletexImports
                                      group ti by ti.ProjectNo into g
                                      select new { ProjectNo = g.Key, HoursSpent = g.Sum(p => p.Hours) }
                                      ) on project.ProjectNo equals tripletexImport.ProjectNo into tripletexImportGroup
                                from hoursSpent in tripletexImportGroup.DefaultIfEmpty()
                                select new
                                {
                                   ProjectName = project.Name,
                                   CustomerName = customer.Name,
                                   HoursSpent = hoursSpent.HoursSpent != null ? hoursSpent.HoursSpent * hourPrice : 0.0,
                                }).Distinct();

         //Query that gets the project name, customer name and the sum of the total sales amount for a SLA project.
         var totalSalesAmountQuery = (from project in dataContext.Projects
                                      join slaProjects in dataContext.SLAProjects on project.ProjectNo equals slaProjects.ProjectNo
                                      join customer in dataContext.Customers on project.CustomerNo equals customer.CustomerNo
                                      join salesFigures in
                                         (
                                            from sf in dataContext.SalesFigures
                                            group sf by sf.ProjectNo into g
                                            select new { ProjectNo = g.Key, TotalSalesAmount = g.Sum(p => p.TotalSalesAmount) }
                                         ) on project.ProjectNo equals salesFigures.ProjectNo into salesFiguresGroup
                                      from salesFigures in salesFiguresGroup.DefaultIfEmpty()
                                      select new
                                      {
                                         ProjectName = project.Name,
                                         CustomerName = customer.Name,
                                         TotalSalesAmount = salesFigures.TotalSalesAmount != null ? salesFigures.TotalSalesAmount : 0.0
                                      }).Distinct();

         if(dataSelection.Equals(BALANCE_AMOUNT)) {
            if (direction.Equals(DESC))
            {
               //Takes the <count> projects from the result in descending order, ordered by BalanceAmount 
               Graph.DataSource = balanceAmountQuery.OrderByDescending(p => p.BalanceAmount).Take(count);
            }
            else
            {
               //Takes the <count> projects from the result in ascending order, ordered by BalanceAmount 
               Graph.DataSource = balanceAmountQuery.OrderBy(p => p.BalanceAmount).Take(count);
            }
         }
         else if (dataSelection.Equals(HOURS_SPENT))
         {
            if (direction.Equals(DESC))
            {
               //Takes the <count> projects from the result in descending order, ordered by Hours spent 
               Graph.DataSource = hoursSpentQuery.OrderByDescending(p => p.HoursSpent).Take(count);
            }
            else
            {
               //Takes the <count> projects from the result in ascending order, ordered by Hours spent 
               Graph.DataSource = hoursSpentQuery.OrderBy(p => p.HoursSpent).Take(count);
            }
         }
         else if (dataSelection.Equals(TOTAL_SALES_AMOUNT))
         {
            if (direction.Equals(DESC))
            {
               //Takes the <count> projects from the result in descending order, ordered by Total Sales Amount 
               Graph.DataSource = totalSalesAmountQuery.OrderByDescending(p => p.TotalSalesAmount).Take(count);
            }
            else
            {
               //Takes the <count> projects from the result in ascending order, ordered by Total Sales Amount
               Graph.DataSource = totalSalesAmountQuery.OrderBy(p => p.TotalSalesAmount).Take(count);
            }
         }

         //Creates the graph
         Series series = new Series("Series1");
         series.ChartArea = "ChartArea1";
         series.ChartType = chartType;
         series.XValueMember = "ProjectName";
         series.YValueMembers = dataSelection;
         series["PointWidth"] = "0.5";
         Graph.Series.Add(series);

         //Binds the data to the Graph
         Graph.DataBind();
      }

      /* 
       * Method that's called when the SelectedIndexChanged event is raised from the DropDownList Server Controls in the
       * GraphTab, when the selected item in a DropDownList is changed.
       * Creates a new graph based on the new selected values.
       */
      protected void GraphTab_SelectionChange(object sender, EventArgs e)
      {
         Graph.Series.Clear();

         SeriesChartType type;
         if(TypeSelect.SelectedValue.Equals(SeriesChartType.Column.ToString())) {
            type = SeriesChartType.Column;
         } else {
            type = SeriesChartType.Pie;
         } 

         CreateGraph(DataSelect.SelectedValue, type, Convert.ToInt32(CountSelect.SelectedValue), DirectionSelect.SelectedValue);
      }

      /*
       * Method that's called when the SelectedIndexChanged event is raised from the CheckBoxList Server Controls on the page,
       * when a checkbox is selected/deselected.
       * Hides/Showes columns in a GridView based on the checked checkboxes.
       */
      protected void Filter_SelectedChanged(object sender, EventArgs e)
      {
         CheckBoxList list = (CheckBoxList) sender;
         GridView gv;

         //Checks which CheckBoxList that raised the event and get the
         //GridView associated with it
         if(list.ID.Equals(OverviewFilter.ID)) {
            gv = OverviewTable;
         }
         else if (list.ID.Equals(SLaTableFilter.ID))
         {
            gv = SLATable;
         }
         else //AddlServicesTable
         {
            gv = AddlServicesTable;
         }

         //Hides/Showes columns in the GridView
         for (int i = 0; i < gv.Columns.Count; i++)
         {
            gv.Columns[i].Visible = list.Items[i].Selected;
         }

         //Reloads the active Tab
         LoadActiveTab(SortExpression, ViewState["sortOrder"].ToString());
      }

      /*
       * Method that's called when the Sorting event is raised from a GridView Server Control,
       * when a columns header is clicked to sort a column.
       * Sets the sorting expression to the sorting expression associated with the event and reloads the
       * active tab to sort it.
       */
      protected void OnSorting(object sender, GridViewSortEventArgs e)
      {
         SortExpression = e.SortExpression;

         LoadActiveTab(SortExpression, SortOrder);
      }

      /**
       * <summary>
       * Method that resets some ViewState information.
       * </summary>
       */
      private void Reset()
      {
         ViewState["sortExpression"] = "";
         ViewState["sortOrder"] = "";
      }

      /**
       * <summary>
       * Method that sorts the GridView sent in as parameter, based on the sortExpression and sortOrder.
       * </summary>
       */
      private void Sort(GridView gridView, string sortExpression, string sortOrder)
      {
         DataTable dt = gridView.DataSource as DataTable;

         if (dt != null)
         {
            DataView dv = dt.DefaultView;
            dv.Sort = sortExpression + " " + sortOrder;

            gridView.DataSource = dv;
            gridView.DataBind();
         }
      }

      /**
       * <summary>
       * Method that returns the header text associated with the SortExpression.
       * </summary>
       */
      private String MapSortExpressionToHeaderText()
      {
         String retVal = null;

         if (SortExpression == PROJECT_NO)
         {
            retVal = "Project No.";
         }
         else if (SortExpression == PROJECT_NAME)
         {
            retVal = "Project Name";
         }
         else if (SortExpression == CUSTOMER_NAME) 
         {
            retVal = "Customer Name";
         }
         else if (SortExpression == PROJECT_MANAGER)
         {
            retVal = "Project Manager";
         }
         else if (SortExpression == PROJECT_START_TIME)
         {
            retVal = "Project Start Time";
         }
         else if (SortExpression == PROJECT_STOP_TIME)
         {
            retVal = "Project Stop Time";
         }
         else if (SortExpression == PROJECT_HOUR_ESTIMATE)
         {
            retVal = "Project Hour Estimate";
         }
         else if (SortExpression == PROJECT_COST_ESTIMATE)
         {
            retVal = "Project Cost Estimate";
         }
         else if (SortExpression == HOURS_SPENT)
         {
            retVal = "Hours Spent";
         }
         else if (SortExpression == TOTAL_SALES_AMOUNT)
         {
            retVal = "Total Sales Amount";
         }
         else if (SortExpression == BALANCE_AMOUNT)
         {
            retVal = "Balance Amount";
         }
         else if (SortExpression == ARTICLE_NO)
         {
            retVal = "Article No.";
         }
         else if (SortExpression == ARTICLE_NAME)
         {
            retVal = "Article Name";
         }

         return retVal;
      }

      /**
       * <summary>
       * Property for the SortOrder. Changes the SortOrder everytime the Property is read. 
       * Sets the property to the specified value when written to.
       * </summary>
       */
      public string SortOrder
      {
         get
         {
            if (ViewState["sortOrder"].Equals(DESC))
            {
               ViewState["sortOrder"] = ASC;
            }
            else
            {
               ViewState["sortOrder"] = DESC;
            }

            return ViewState["sortOrder"].ToString();
         }
         set
         {
            ViewState["sortOrder"] = value;
         }
      }


      /**
       * <summary>
       * Property for the SortExpression.
       * Returns the SortExpression when read, sets the SortExpression when written to.
       * </summary>
       */
      public string SortExpression
      {
         get
         {
            return ViewState["sortExpression"].ToString();
         }
         set
         {
            ViewState["sortExpression"] = value;
         }
      }
   }
}
