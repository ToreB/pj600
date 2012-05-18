using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using no.nith.pj600.dashboard.Code;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Configuration;

namespace no.nith.pj600.dashboard
{
   public partial class SearchResults : System.Web.UI.Page
   {
      private const string SEARCH_BOX = "SearchInput";
      private DatabaseClassesDataContext dataContext;

      protected void Page_Load(object sender, EventArgs e)
      {
         String searchInput;

         //Search from another page
         if (Page.PreviousPage != null)
         {          
            searchInput = ((TextBox) Page.PreviousPage.Master.FindControl(SEARCH_BOX)).Text;
            performSearch(searchInput);
         }
         else 
         {
            //Search from the SearchResult page
            if (Page.IsPostBack && !Request.Form["__EVENTTARGET"].Contains("Filter"))
            {
               searchInput = ((TextBox)Page.Master.FindControl(SEARCH_BOX)).Text;
               performSearch(searchInput);
            }
         }
      }

      /**
       * <summary>
       * Method that performs the search for the specified input.
       * </summary>
       */
      private void performSearch(string input)
      {
         SearchInputHidden.Value = input;
         
         //Resets the page
         MessagePanel.Visible = false;
         Filter.Visible = false;
         Results.DataSource = null;
         Results.DataBind();

         dataContext = new DatabaseClassesDataContext(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);

         //Gets the HourPrice that's configured in the Web.config's AppSettings section.
         double hourPrice = Convert.ToDouble(ConfigurationManager.AppSettings["HourPrice"]);

         //Query that gets the project number, project name, customer name, project manager, project start time,
         //project stop time, hours spent, sum of total sales amount and the latest balance for each SLA project that
         //matches the input parameter.
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
                      where SqlMethods.Like(project.ProjectNo.ToString(), string.Format("%{0}%", input)) ||
                      SqlMethods.Like(project.Name.ToLower(), string.Format("%{0}%", input.ToLower())) ||
                      SqlMethods.Like(employee.Name.ToLower(), string.Format("%{0}%", input.ToLower()))
                      select new {
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

         var list = query.ToList();

         if (list.Count != 0)
         {
            Filter.Visible = true;
            //Binds the data to the GridView
            Results.DataSource = list;
            Results.DataBind();
         }
         else
         {
            MessagePanel.Visible = true;
         }
      }

      /*
       * Method that's called when the SelectedIndexChanged event is raised from the CheckBoxList Server Controls on the page,
       * when a checkbox is selected/deselected.
       * Hides/Showes columns in a GridView based on the checked checkboxes.
       */
      protected void Filter_SelectedChanged(object sender, EventArgs e)
      {
         CheckBoxList list = (CheckBoxList)sender;

         //Hides/Showes columns in the GridView
         for (int i = 0; i < Results.Columns.Count; i++)
         {
            Results.Columns[i].Visible = list.Items[i].Selected;
         }

         performSearch(SearchInputHidden.Value);
      }
   }
}