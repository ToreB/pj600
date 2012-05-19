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
      private const string SEARCH_DEFAULT_TEXT = "Search...";

      protected void Page_Load(object sender, EventArgs e)
      {
         //Adds a link in the navigation menu if the current user is an admin
         if (HttpContext.Current.User.IsInRole("Admin"))
         //if(Roles.IsUserInRole("Admin"))
         {
            MenuItem item = new MenuItem("Admin Panel", null, null, ADMINPANEL_PATH);
            NavigationMenu.Items.AddAt(2, item);
         }

         //Shows the SearchPanel if a user is logged in
         if (Membership.GetUser() != null)
         {
            SearchPanel.Visible = true;

            //Register events that makes the SearchInput go blank when it gets focus,
            //and re-add 'Search...' when the SearchInput loses focus without anything beeing written.
            SearchInput.Attributes.Add("onfocus", string.Format("if(this.value == '{0}') this.value = '';", SEARCH_DEFAULT_TEXT));
            SearchInput.Attributes.Add("onblur", string.Format("if(this.value == '') this.value = '{0}';", SEARCH_DEFAULT_TEXT));
            }
      }

      /*
       * Method that's called when the LoggedOut event is raised from the LoginStatus Server Controll, HeadLoginStatus,
       * when the logout link is clicked.
       * Logs that a user has logged out.
       */
      protected void OnLoggedOut(object sender, EventArgs e)
      {
         log.Info(Membership.GetUser().UserName + " has logged out.");
      }
   }
}
