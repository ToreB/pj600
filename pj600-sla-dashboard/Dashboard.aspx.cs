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
   public partial class Dashboard : System.Web.UI.Page
   {
      private static readonly ILog log = LogManager.GetLogger(typeof(Dashboard));
      private DatabaseClassesDataContext dataContext;

      protected void Page_Load(object sender, EventArgs e)
      {
         //Loads the Overview tab when the page loads, if it's not a postback.
         if (!Page.IsPostBack)
         {
            LoadOverviewTab();
         }
         else
         {
            LoadActiveTab();
         }
      }

      protected void OnPageIndexChanging(object sender, EventArgs e)
      {
         ((GridView)sender).PageIndex = ((GridViewPageEventArgs)e).NewPageIndex;
      }

      protected void OnPageIndexChanged(object sender, EventArgs e)
      {
         ((GridView)sender).DataBind();
      }

      protected void TabContainerTabChange(object sender, EventArgs e)
      {
         LoadActiveTab();
      }

      private void LoadActiveTab()
      {
         if (TabContainer.ActiveTab.ID.Equals("OverviewTab"))
         {
            LoadOverviewTab();
         }
         else if (TabContainer.ActiveTab.ID.Equals("SLATab"))
         {
            LoadSLATab();
         }
         else if (TabContainer.ActiveTab.ID.Equals("AddlServicesTab"))
         {
            LoadAddlServicesTab();
         }
         else
         {
            LoadGraphsTab();
         }
      }

      private void LoadOverviewTab()
      {
         /*dataContext = new DatabaseClassesDataContext();
         var query = from Customer in dataContext.Customers 
                     where Customer.Name != null
                     select Customer;
         
         OverviewTable.DataSource = query;
         OverviewTable.DataBind();*/
      }

      private void LoadSLATab()
      {
         dataContext = new DatabaseClassesDataContext();
         var query = from c in dataContext.Customers
                     join p in dataContext.Projects on c.CustomerNo equals p.CustomerNo
                     where p.ProjectNo == 10001 ||
                     p.ProjectNo == 10002 ||
                     p.ProjectNo == 10005 ||
                     p.ProjectNo == 10006 ||
                     p.ProjectNo == 10007 ||
                     p.ProjectNo == 10023 ||
                     p.ProjectNo == 10032 ||
                     p.ProjectNo == 10051 ||
                     p.ProjectNo == 10054 ||
                     p.ProjectNo == 10057 ||
                     p.ProjectNo == 10062 ||
                     p.ProjectNo == 10107 ||
                     p.ProjectNo == 10116 ||
                     p.ProjectNo == 10121 ||
                     p.ProjectNo == 10133 ||
                     p.ProjectNo == 10164 ||
                     p.ProjectNo == 10178 ||
                     p.ProjectNo == 10222 ||
                     p.ProjectNo == 10227 ||
                     p.ProjectNo == 10232 ||
                     p.ProjectNo == 10343 ||
                     p.ProjectNo == 10344 ||
                     p.ProjectNo == 10349 ||
                     p.ProjectNo == 10370 ||
                     p.ProjectNo == 10431 ||
                     p.ProjectNo == 10436 ||
                     p.ProjectNo == 10525 ||
                     p.ProjectNo == 10533 ||
                     p.ProjectNo == 10565 ||
                     p.ProjectNo == 10583 ||
                     p.ProjectNo == 10686 ||
                     p.ProjectNo == 10694 ||
                     p.ProjectNo == 10811 ||
                     p.ProjectNo == 10866
                     orderby p.ProjectNo ascending
                     select new { CustomerName = c.Name, ProjectNo = p.ProjectNo, ProjectName = p.Name };

         SLATable.DataSource = query.Distinct();
         SLATable.DataBind();
      }

      private void LoadAddlServicesTab()
      {

      }

      private void LoadGraphsTab()
      {

      }
   }
}
