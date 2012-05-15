using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Web.Security;
using no.nith.pj600.dashboard.Code;

namespace no.nith.pj600.dashboard.Account
{
   public partial class ResetPassword : System.Web.UI.Page
   {
      private static readonly ILog log = LogManager.GetLogger(typeof(ResetPassword));
      private MembershipUser user;
      private string email;

      protected void Page_Load(object sender, EventArgs e)
      {
         
      }

      /**
       * <summary>
       * Method that checks if the email in the EmailInput is registered to a user.
       * </summary>
       */
      private bool VerifyEmail()
      {
         email = ((TextBox) ResetPasswordView.FindControl("EmailInput")).Text;
         string username = Membership.GetUserNameByEmail(email);        

         if (username != null)
         {
            user = Membership.GetUser(username);
            return true;
         }
         else
         {
            return false;
         }
      }

      /*
       * Method that's called when the Click event is fired from the Button Server Control, ResetButton,
       * when the button is clicked.
       * Resets a user's password and sends an email confirmation.
       */
      protected void ResetButton_Click(object sender, EventArgs e)
      {
         Label message = (Label) ResetPasswordView.FindControl("Message");

         if (VerifyEmail())
         {
            try
            {
               string newPassword = user.ResetPassword();

               String mailBody = "Your new password is: " + newPassword;
               bool success = MailSender.Send(email, "Password Reset", mailBody);

               if (success)
               {
                  message.CssClass = "infoMessage";
                  message.Text = "Your password has been reset. Your new password has been sent to your email.";
               }
               else
               {
                  message.CssClass = "errorMessage";
                  message.Text = "Something went wrong while trying to send email. Please try again later.";
               }
            }
            catch (Exception ex)
            {
               message.CssClass = "errorMessage";
               message.Text = "Something went wrong: " + ex.Message;
            }
         }
         else //VerifyEmail returns false
         {
            message.CssClass = "errorMessage";
            message.Text = "A user with that email was not found.";
         }
      }
   }
}