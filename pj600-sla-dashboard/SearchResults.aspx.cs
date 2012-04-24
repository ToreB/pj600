using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using no.nith.pj600.dashboard.Code;
using System.Data;
using System.Data.Linq.SqlClient;

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
            if (Page.IsPostBack)
            {
               searchInput = ((TextBox)Page.Master.FindControl(SEARCH_BOX)).Text;
               performSearch(searchInput);
            }
         }
      }

      private void performSearch(string input)
      {
         dataContext = new DatabaseClassesDataContext();

         var query = (from project in dataContext.Projects
                      join customer in dataContext.Customers on project.CustomerNo equals customer.CustomerNo
                      join slaProject in dataContext.SLAProjects on project.ProjectNo equals slaProject.ProjectNo
                      join employee in dataContext.Employees on project.PMEmployeeNo equals employee.EmployeeNo 
                      where SqlMethods.Like(project.ProjectNo.ToString(), string.Format("%{0}%", input)) ||
                      SqlMethods.Like(project.Name.ToLower(), string.Format("%{0}%", input.ToLower())) ||
                      SqlMethods.Like(employee.Name.ToLower(), string.Format("%{0}%", input.ToLower()))
                      select new { 
                         ProjectNo = project.ProjectNo, 
                         ProjectName = project.Name, 
                         CustomerName = customer.Name,
                         ProjectManager = employee.Name
                      }).Distinct();

         Results.DataSource = query;
         Results.DataBind();
      }
      
   }
}