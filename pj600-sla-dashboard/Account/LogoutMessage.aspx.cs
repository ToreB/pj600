using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace no.nith.pj600.dashboard
{
   public partial class LogoutMessage : System.Web.UI.Page
   {
      protected void Page_Load(object sender, EventArgs e)
      {
         if (Membership.GetUser() == null)
         {
            Message.CssClass = "infoMessage";
            Message.Text = "You have successfully been logged out.";
         }
         else
         {
            Message.CssClass = "errorMessage";
            Message.Text = "You are still logged in.";
         }
      }
   }
}