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
   public partial class Login : System.Web.UI.Page
   {
      private static readonly ILog log = LogManager.GetLogger(typeof(Login));

      protected void Page_Load(object sender, EventArgs e)
      {
         
      }

      /*
       * Method that's run after a successfull login
       */ 
      protected void OnLoggedIn(object sender, EventArgs e)
      {
         log.Info(LoginUser.UserName + " has logged in.");
         
      }
   }
}
