using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace no.nith.pj600.dashboard.Admin
{
   public partial class ManageSLAProjects : System.Web.UI.Page
   {
      protected void Page_Load(object sender, EventArgs e)
      {

      }

      /*
       * Method that checks for exceptions, and handles them, when the SqlDataSource's Inserted, Updated, Selected and Deleted
       * events are raised.
       */
      protected void CheckForExceptions(object sender, SqlDataSourceStatusEventArgs e)
      {
         //Check if there's an exception
         if (e.Exception != null)
         {
            MessagePanel.Visible = true;
            Message.Text = e.Exception.Message;

            e.ExceptionHandled = true;
         }
      }
   }
}