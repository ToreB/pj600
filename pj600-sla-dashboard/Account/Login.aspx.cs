using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

using log4net;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace no.nith.pj600.dashboard.Account
{
   public partial class Login : System.Web.UI.Page
   {
      private static readonly ILog log = LogManager.GetLogger(typeof(Login));

      protected void Page_Load(object sender, EventArgs e)
      {
         if (!Page.IsPostBack)
         {
            if (Request.Params["logout"] != null)
            {
               LogoutMessagePanel.Visible = true;
            }
         }
      }

      /*
       * Method that's run after a successfull login
       */ 
      protected void OnLoggedIn(object sender, EventArgs e)
      {
         log.Info(LoginUser.UserName + " has logged in.");
         
      }

      protected void OnLoginError(object sender, EventArgs e)
      {
         MembershipUser user = Membership.GetUser(LoginUser.UserName);

         if(user != null) {           

            if (user.IsLockedOut)
            {
               AccountStatusPanel.Visible = true;
               AccountStatusLabel.Text = "You have exceeded the allowed amount of login attempts and your account has been locked.<br />" +
                                          "Please contact an Admin to have your account unlocked.";

               //Logs only just after the account gets locked
               DateTime now = DateTime.Now;
               now = now.Subtract(new TimeSpan(0, 0, 2));

               if (now < user.LastLockoutDate)
               {
                  log.Info("The user with the username '" + user.UserName + "' has been locked out due to exceeding the allowed amount of login attempts.");
               }
            }
            else
            {
               //Get the failed password attempt count from the database
               SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
               con.Open();

               SqlCommand command = new SqlCommand(
                  string.Format("SELECT FailedPasswordAttemptCount FROM aspnet_Membership WHERE email = '{0}'", user.Email), con);
               SqlDataReader reader = command.ExecuteReader();

               //Should always return 1 column as result, so this should be safe
               reader.Read();
               int failedAttempts = (int) reader[0];               
               int allowedAttepts = Membership.MaxInvalidPasswordAttempts;

               ((Literal)LoginUser.FindControl("FailureAttemptsLiteral")).Text = "<br />You have " + (allowedAttepts - failedAttempts) + " login attempts left.";

               reader.Close();
               con.Close();
            }
         }
      }
   }
}
