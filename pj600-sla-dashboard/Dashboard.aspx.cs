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

      protected void Page_Load(object sender, EventArgs e)
      {
         /*
         DatabaseClassesDataContext dataContext = new DatabaseClassesDataContext();
         var query = (from Customer in dataContext.Customers select Customer).Take(10);

         GridView1.DataSource = query;
         GridView1.DataBind();
         */

        /* 
        DataSet ds = ExcelHandler.GetDataSet("Excel.xlsx");

        if (ds != null)
         {
            GridView1.DataSource = ds.Tables[0].DefaultView;
            GridView1.DataBind();
         }
        */
      }
   }
}
