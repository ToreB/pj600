using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

using log4net;

namespace no.nith.pj600.dashboard
{
   public partial class SiteMaster : System.Web.UI.MasterPage
   {
      private static readonly ILog log = LogManager.GetLogger(typeof(SiteMaster));
      private const string ADMINPANEL_PATH = "~/Admin/AdminPanel.aspx";

      protected void Page_Load(object sender, EventArgs e)
      {
         //Adds a link in the navigation menu if the current user is an admin
         if (Context.User.IsInRole("Admin"))
         {
            MenuItem item = new MenuItem("Admin Panel", null, null, ADMINPANEL_PATH);
            NavigationMenu.Items.AddAt(2, item);
         }

         if (Membership.GetUser() != null)
         {
            SearchPanel.Visible = true;

            //Register events that makes the SearchInput go blank when it gets focus,
            //and re-add 'Search...' when the SearchInput loses focus without anything beeing written.
            SearchInput.Attributes.Add("onfocus", "if(this.value == 'Search...') this.value = '';");
            SearchInput.Attributes.Add("onblur", "if(this.value == '') this.value = 'Search...';");
         }
      }

      protected void OnLoggedOut(object sender, EventArgs e)
      {
         log.Info(Membership.GetUser().UserName + " has logged out.");

         //LogoutPanel.Visible = true;
         //Context.Response.Redirect("Default.aspx");
      }
   }
}
