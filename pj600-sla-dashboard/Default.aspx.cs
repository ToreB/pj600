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

      protected void Page_Load(object sender, EventArgs e)
      {

         //Loads the Overview tab when the page loads, if it's not a postback.
         if (!Page.IsPostBack)
         {
            ResetViewState();
            LoadOverviewTab("", "");  
         }
         else
         {
            LoadActiveTab(SortExpression, ViewState["sortOrder"].ToString());
         }
      }

      protected void OnPageIndexChanging(object sender, EventArgs e)
      {
         ((GridView)sender).PageIndex = ((GridViewPageEventArgs)e).NewPageIndex;
         ((GridView)sender).DataBind();
      }

      protected void TabContainerTabChange(object sender, EventArgs e)
      {
         ResetViewState();
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

      }

      private void LoadGraphsTab(string sortExpression, string sortOrder)
      {

      }

      protected void SLATable_OnSorting(object sender, GridViewSortEventArgs e)
      {
         SortExpression = e.SortExpression;
         LoadActiveTab(e.SortExpression, SortOrder);
      }

      private void ResetViewState()
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
