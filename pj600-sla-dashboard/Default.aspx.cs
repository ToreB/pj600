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

namespace no.nith.pj600.dashboard
{
   public partial class _Default : System.Web.UI.Page
   {
      private static readonly ILog log = LogManager.GetLogger(typeof(_Default));
      private DatabaseClassesDataContext dataContext;

      private const string ASC = "asc";
      private const string DESC = "desc";

      //DataTable columns
      protected const string PROJECT_NO = "ProjectNo";
      protected const string PROJECT_NAME = "ProjectName";
      protected const string CUSTOMER_NAME = "CustomerName";
      protected const string PROJECT_MANAGER = "ProjectManager";
      protected const string PROJECT_START_TIME = "ProjectStartTime";
      protected const string PROJECT_STOP_TIME = "ProjectStopTime";
      protected const string PROJECT_HOUR_ESTIMATE = "ProjectHourEstimate";
      protected const string PROJECT_COST_ESTIMATE = "ProjectCostEstimate";
      protected const string HOURS_SPENT = "HoursSpent";
      protected const string TOTAL_SALES_AMOUNT = "TotalSalesAmount";
      protected const string BALANCE_AMOUNT = "BalanceAmount";
      protected const string ARTICLE_NAME = "ArticleName";
      protected const string ARTICLE_NO = "ArticleNo";

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

      protected void RowCreated(object sender, GridViewRowEventArgs e)
      {
         GridView gridView = (GridView)sender;

         if (e.Row.RowType == DataControlRowType.Header)
         {
            foreach (TableCell cell in e.Row.Cells)
            {
               if (cell.HasControls())
               {
                  LinkButton button = cell.Controls[0] as LinkButton;

                  if (button != null)
                  {
                     if (button.Text.Equals(MapSortExpressionToHeaderText()))
                     {
                        AddSortImage(cell);
                     }  
                  }
               }
            }
         }
      }

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

            //((TextBox)Page.Master.FindControl("SearchInput")).Text = SortExpression + " " + ViewState["sortOrder"];
            
            cell.Controls.Add(sortImage);
         }
      }

      protected void OnPageIndexChanging(object sender, EventArgs e)
      {
         ((GridView)sender).PageIndex = ((GridViewPageEventArgs)e).NewPageIndex;
         //((GridView)sender).DataBind();
         LoadActiveTab(SortExpression, ViewState["sortOrder"].ToString());
         //((TextBox)Page.Master.FindControl("SearchInput")).Text = SortExpression + " " + ViewState["sortOrder"];
      }

      protected void TabContainerTabChange(object sender, EventArgs e)
      {
         Reset();
         LoadActiveTab("", "");
         //((TextBox)Page.Master.FindControl("SearchInput")).Text = "TabChange";
      }

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
            LoadGraphsTab(sortExpression, sortOrder);
         }
      }

      private void LoadOverviewTab(string sortExpression, string sortOrder)
      {
         dataContext = new DatabaseClassesDataContext();

         double hourPrice = 500;

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
                            group b by b.ProjectNo into g
                            select new { ProjectNo = g.Key, BalanceAmount = g.Sum(p => p.Amount) }
                         ) on project.ProjectNo equals balance.ProjectNo into balanceGroup
                      from balance in balanceGroup.DefaultIfEmpty()
                      select new OverviewTableStruct
                      {
                         ProjectNo = (int)project.ProjectNo,
                         ProjectName = project.Name,
                         CustomerName = customer.Name,
                         ProjectManager = employee.Name,
                         ProjectStartTime = (DateTime)project.StartTime,
                         ProjectStopTime = (DateTime)project.StopTime,
                         ProjectHourEstimate = (double)project.HourEstimate,
                         ProjectCostEstimate = (double)project.CostEstimate,
                         HoursSpent = hoursSpent.HoursSpent != null ? hoursSpent.HoursSpent * hourPrice : 0.0,
                         TotalSalesAmount = salesFigures.TotalSalesAmount != null ? salesFigures.TotalSalesAmount : 0.0,
                         BalanceAmount = balance.BalanceAmount != null ? balance.BalanceAmount : 0.0
                      }).Distinct();

         /*List<OverviewTableStruct> result = query.ToList();

         string[] columns = {PROJECT_NO, PROJECT_NAME, CUSTOMER_NAME, PROJECT_MANAGER, PROJECT_START_TIME, PROJECT_STOP_TIME,
                             PROJECT_HOUR_ESTIMATE, PROJECT_COST_ESTIMATE, HOURS_SPENT, TOTAL_SALES_AMOUNT, BALANCE_AMOUNT};         
         string[,] data = new string[result.Count, columns.Length];
        
         for (int row = 0; row < result.Count; row++)
         {
            OverviewTableStruct item = result[row];
            data[row, 0] = item.ProjectNo.ToString();
            data[row, 1] = item.ProjectName;
            data[row, 2] = item.CustomerName;
            data[row, 3] = item.ProjectManager;
            data[row, 4] = item.ProjectStartTime.ToString();
            data[row, 5] = item.ProjectStopTime.ToString();
            data[row, 6] = item.ProjectHourEstimate.ToString();
            data[row, 7] = item.ProjectCostEstimate.ToString();
            data[row, 8] = item.HoursSpent.ToString();
            data[row, 9] = item.TotalSalesAmount.ToString();
            data[row, 10] = item.BalanceAmount.ToString();
         }*/
         DataTable dt = new DataTable();
         dt.Columns.Add(PROJECT_NO, Type.GetType("System.Int32"));
         dt.Columns.Add(PROJECT_NAME);
         dt.Columns.Add(CUSTOMER_NAME);
         dt.Columns.Add(PROJECT_MANAGER);
         dt.Columns.Add(PROJECT_START_TIME, Type.GetType("System.DateTime"));
         dt.Columns.Add(PROJECT_STOP_TIME, Type.GetType("System.DateTime"));
         dt.Columns.Add(PROJECT_HOUR_ESTIMATE, Type.GetType("System.Double"));
         dt.Columns.Add(PROJECT_COST_ESTIMATE, Type.GetType("System.Double"));
         dt.Columns.Add(HOURS_SPENT, Type.GetType("System.Double"));
         dt.Columns.Add(TOTAL_SALES_AMOUNT, Type.GetType("System.Double"));
         dt.Columns.Add(BALANCE_AMOUNT, Type.GetType("System.Double"));

         foreach (var row in query)
         {
            DataRow newRow = dt.NewRow();

            newRow[PROJECT_NO] = row.ProjectNo;
            newRow[PROJECT_NAME] = row.ProjectName;
            newRow[CUSTOMER_NAME] = row.CustomerName;
            newRow[PROJECT_MANAGER] = row.ProjectManager;
            newRow[PROJECT_START_TIME] = row.ProjectStartTime;
            newRow[PROJECT_STOP_TIME] = row.ProjectStopTime;
            newRow[PROJECT_HOUR_ESTIMATE] = row.ProjectHourEstimate;
            newRow[PROJECT_COST_ESTIMATE] = row.ProjectCostEstimate;
            newRow[HOURS_SPENT] = row.HoursSpent;
            newRow[TOTAL_SALES_AMOUNT] = row.TotalSalesAmount;
            newRow[BALANCE_AMOUNT] = row.BalanceAmount;

            dt.Rows.Add(newRow);
         }

         OverviewTable.DataSource = dt;
         OverviewTable.DataBind();

         if (sortExpression != string.Empty)
         {
            Sort(OverviewTable, sortExpression, sortOrder);
         }
         else
         {
            Sort(OverviewTable, "ProjectNo", ASC);
         }
      }

      private void LoadSLATab(string sortExpression, string sortOrder)
      {
         dataContext = new DatabaseClassesDataContext();

         var mainQuery = (from project in dataContext.Projects
                      join customer in dataContext.Customers on project.CustomerNo equals customer.CustomerNo
                      join slaProjects in dataContext.SLAProjects on project.ProjectNo equals slaProjects.ProjectNo
                      join employee in dataContext.Employees on project.PMEmployeeNo equals employee.EmployeeNo
                      join balance in
                         (
                            from b in dataContext.Balances
                            group b by b.ProjectNo into g
                            select new { ProjectNo = g.Key, BalanceAmount = g.Sum(p => p.Amount) }
                         ) on project.ProjectNo equals balance.ProjectNo into balanceGroup
                      from balance in balanceGroup.DefaultIfEmpty()
                      orderby project.ProjectNo ascending
                      select new {        
                         ProjectNo = project.ProjectNo, 
                         ProjectName = project.Name,
                         CustomerName = customer.Name,
                         ProjectManager = employee.Name,
                         BalanceAmount = balance.BalanceAmount != null ? balance.BalanceAmount : 0
                      }).Distinct();

         /*int currentYear = DateTime.Now.Year;
         var balancePerMonth = from slaProject in dataContext.SLAProjects
                               join balance in
                                  (
                                   from b in dataContext.Balances
                                   where b.Year == currentYear
                                   group b by new { b.ProjectNo, b.Period, b.Year } into g
                                   select new { ProjectNo = g.Key.ProjectNo, Period = g.Key.Period, Year = g.Key.Year, BalancePerMonth = g.Sum(p => p.Amount) }
                                  ).Union(
                                 from  b in dataContext.Balances
                                 where b.Year == (currentYear - 1) &&
                                 !(from b2 in dataContext.Balances where b2.Year == currentYear select b2.Period).Contains(b.Period)
                                 group b by new { b.ProjectNo, b.Period, b.Year } into g
                                 select new { ProjectNo = g.Key.ProjectNo, Period = g.Key.Period, Year = g.Key.Year, BalancePerMonth = g.Sum(p => p.Amount) }
                               ) on slaProject.ProjectNo equals balance.ProjectNo into balanceSlaProjectsJoin
                               from selection in balanceSlaProjectsJoin.DefaultIfEmpty()
                               orderby selection.ProjectNo ascending, selection.Year descending, selection.Period ascending
                               select new
                               {
                                  Period = selection.Period,
                                  Year = selection.Year,
                                  BalancePerMonth = selection.BalancePerMonth
                               };

         var balancePerMonthList = balancePerMonth.ToList();*/

         DataTable dt = new DataTable();
         dt.Columns.Add(PROJECT_NO, Type.GetType("System.Int32"));
         dt.Columns.Add(PROJECT_NAME);
         dt.Columns.Add(CUSTOMER_NAME);
         dt.Columns.Add(PROJECT_MANAGER);
         dt.Columns.Add(BALANCE_AMOUNT, Type.GetType("System.Double"));

         /*DateTime date = new DateTime(currentYear, 1, 1);
         DateTime currentTime = DateTime.Now;
         string[] headers = new String[12];
         for (int i = 1; i <= 12; i++)
         {
            string header = date.ToString("MMM");
            headers[i - 1] = header;
            dt.Columns.Add(header);*/

            /*BoundField column = new BoundField();
            columnataField = header;
            column.HeaderText = header;
            column.SortExpression = header;
            SLATable.Columns.Add(column);*/

            /*date = date.AddMonths(1);
         }*/

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

         SLATable.DataSource = dt;
         SLATable.DataBind();

         if (sortExpression != string.Empty)
         {
            Sort(SLATable, sortExpression, sortOrder);
         }
         else
         {
            Sort(SLATable, "ProjectNo", ASC);
         }
      }

      private void LoadAddlServicesTab(string sortExpression, string sortOrder)
      {
         dataContext = new DatabaseClassesDataContext();

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

         DataTable dt = new DataTable();
         dt.Columns.Add(PROJECT_NO, Type.GetType("System.Int32"));
         dt.Columns.Add(PROJECT_NAME);
         dt.Columns.Add(CUSTOMER_NAME);
         dt.Columns.Add(ARTICLE_NO);
         dt.Columns.Add(ARTICLE_NAME);
         dt.Columns.Add(TOTAL_SALES_AMOUNT, Type.GetType("System.Double"));

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

         AddlServicesTable.DataSource = dt;
         AddlServicesTable.DataBind();

         if (sortExpression != string.Empty)
         {
            Sort(AddlServicesTable, sortExpression, sortOrder);
         }
         else
         {
            Sort(AddlServicesTable, "ProjectNo", ASC);
         }
      }

      private void LoadGraphsTab(string sortExpression, string sortOrder)
      {

      }

      protected void OnSorting(object sender, GridViewSortEventArgs e)
      {
         SortExpression = e.SortExpression;

         LoadActiveTab(SortExpression, SortOrder);
         //Sort((GridView)sender, SortExpression, SortOrder); 
      }

      private void Reset()
      {
         ViewState["sortExpression"] = "";
         ViewState["sortOrder"] = "";
      }

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

      private DataTable CreateDataTable(string[] columns, string[,] data)
      {
         DataTable dt = new DataTable();

         foreach (string column in columns)
         {
            dt.Columns.Add(column);
         }

         for (int row = 0; row < data.GetLength(0); row++)
         {
            DataRow newRow = dt.NewRow();

            for (int column = 0; column < data.GetLength(1); column++)
            {
               newRow[dt.Columns[column]] = data[row, column];
            }

            dt.Rows.Add(newRow);
         }

         return dt;
      }

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
