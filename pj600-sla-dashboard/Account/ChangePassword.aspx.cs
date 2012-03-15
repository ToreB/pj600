using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using log4net;

namespace no.nith.pj600.dashboard.Account
{
   public partial class ChangePassword : System.Web.UI.Page
   {
      private static readonly ILog log = LogManager.GetLogger(typeof(ChangePassword));

      protected void Page_Load(object sender, EventArgs e)
      {

      }

      protected void OnChangedPassword(object sender, EventArgs e)
      {
         string username = Membership.GetUser().UserName;
         log.Info(username + " changed his/her password.");
      }
   }
}
