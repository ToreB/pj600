using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace no.nith.pj600.dashboard
{
   public partial class SiteMaster : System.Web.UI.MasterPage
   {
      protected void Page_Load(object sender, EventArgs e)
      {
         //Adds a link in the navigation menu if the current user is an admin
         if (Context.User.IsInRole("Admin"))
         {
            MenuItem item = new MenuItem("Admin Panel", null, null, "~/AdminPanel.aspx");
            NavigationMenu.Items.Add(item);
         }
      }

      /*protected void HeadLoginStatus_LoggedOut(object sender, EventArgs e)
      {
         LogoutPanel.Visible = true;
         //Context.Response.Redirect("Default.aspx");
      }*/
   }
}
