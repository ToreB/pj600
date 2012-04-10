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

      protected void Page_Load(object sender, EventArgs e)
      {
         //Loads the Overview tab when the page loads, if it's not a postback.
         if (!Page.IsPostBack)
         {
            ViewState["sortExpression"] = "";
            ViewState["sortOrder"] = "";
            LoadOverviewTab("", "");
         }
         else
         {
            LoadActiveTab("", "");
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
         LoadActiveTab("", "");
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
         /*dataContext = new DatabaseClassesDataContext();
         var query = from Customer in dataContext.Customers 
                     where Customer.Name != null
                     select Customer;
         
         OverviewTable.DataSource = query;
         OverviewTable.DataBind();*/
      }

      private void LoadSLATab(string sortExpression, string sortOrder)
      {
         dataContext = new DatabaseClassesDataContext();

         var query = (from c in dataContext.Customers
                      join p in dataContext.Projects on c.CustomerNo equals p.CustomerNo
                      join slaProjects in dataContext.SLAProjects on p.ProjectNo equals slaProjects.ProjectNo
                      orderby p.ProjectNo
                      select new { CustomerName = c.Name, ProjectNo = p.ProjectNo, ProjectName = p.Name }).Distinct();

         //Creates a new DataTable and fills it with the rows from the query
         DataTable dt = new DataTable();
         dt.Columns.Add("CustomerName");
         dt.Columns.Add("ProjectNo");
         dt.Columns.Add("ProjectName");

         foreach (var row in query)
         {
            DataRow newRow = dt.NewRow();
            newRow["CustomerName"] = row.CustomerName;
            newRow["ProjectNo"] = row.ProjectNo;
            newRow["ProjectName"] = row.ProjectName;
            dt.Rows.Add(newRow);
         }

         //Gets the DefaultView from the DataTable and sorts it if the sort expression isn't empty
         DataView dv = dt.DefaultView;
         if (sortExpression != string.Empty)
         {
            dv.Sort = sortExpression + " " + sortOrder;
         }
         else
         {
            dv.Sort = "ProjectNo asc";
         }

         //Sets and binds the GridViews data
         SLATable.DataSource = dv;
         SLATable.DataBind();
      }

      private void LoadAddlServicesTab(string sortExpression, string sortOrder)
      {

      }

      private void LoadGraphsTab(string sortExpression, string sortOrder)
      {

      }

      protected void SLATable_OnSorting(object sender, GridViewSortEventArgs e)
      {
         SortExpression = e.SortExpression;
         LoadActiveTab(e.SortExpression, SortOrder);

      }

      public string SortOrder
      {
         get
         {
            if (ViewState["sortOrder"].Equals("desc"))
            {
               ViewState["sortOrder"] = "asc";
            }
            else
            {
               ViewState["sortOrder"] = "desc";
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
