﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using no.nith.pj600.dashboard.Code;
using log4net;
using System.Text.RegularExpressions;
using System.Collections;

namespace no.nith.pj600.dashboard.Account
{
   public partial class Register : System.Web.UI.Page
   {
      private static readonly ILog log = LogManager.GetLogger(typeof(Register));

      /* RegEx from http://msdn.microsoft.com/en-us/library/01escwtf(v=vs.95).aspx */
      private const string EMAIL_FORMAT = @"^(?("")(""[^""]+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
                                          @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$";

      protected void Page_Load(object sender, EventArgs e)
      {
         //RegisterUser.ContinueDestinationPageUrl = Request.QueryString["ReturnUrl"];  
      }

      /*
      protected void RegisterUser_CreatedUser(object sender, EventArgs e)
      {
         FormsAuthentication.SetAuthCookie(RegisterUser.UserName, false);

         string continueUrl = RegisterUser.ContinueDestinationPageUrl;
         if (String.IsNullOrEmpty(continueUrl))
         {
            continueUrl = "~/";
         }
         Response.Redirect(continueUrl);
      }
      */

      protected void CreateUser(object sender, EventArgs e)
      {
         string email = Email.Text;

         if (!IsValidEmail(email))
         {
            Message.CssClass = "errorMessage";
            Message.Text = "Invalid E-mail format.";
            return;
         }

         string username = UserName.Text;
         string password = Membership.GeneratePassword(8, 0);

         try
         {
            MembershipUser user = Membership.CreateUser(username, password, email);
            log.Info("A new user with the username '" + username + "' has been created.");

            ArrayList assignedRoles = new ArrayList();
            foreach (ListItem li in RolesList.Items)
            {
               if (li.Selected == true)
               {
                  string role = li.Text;
                  assignedRoles.Add(role);
                  Roles.AddUsersToRole(new string[]{username}, role);
                  log.Info(string.Format("'{0}' has been assigned to the role '{1}'", username, role));
               }
            }

            string mailBody = string.Format("Welcome to 99X Dashboard!\n\n" + 
                                         "Your login information is:\nUsername: {0}\nPassword: {1}\n", username, password);

            if (assignedRoles.Count != 0)
            {
               mailBody += "\nYou have been assigned the following roles:";

               foreach (string role in assignedRoles)
               {
                  mailBody += " " + role;
               }

               mailBody += ".";
            }

            bool success = MailSender.Send(email, "Welcome to 99X Dashboard", mailBody);

            if (success)
            {
               Message.CssClass = "infoMessage";
               Message.Text = "User creation successful. An email with the account info has been sent to the registered email.";
            }
            else
            {
               Message.CssClass = "errorMessage";
               Message.Text = "User was created, but email with account info was not sent to the registered email.";
            }
         }
         catch (MembershipCreateUserException mcuEx)
         {
            Message.CssClass = "errorMessage";
            Message.Text = "User creation failed: " + mcuEx.StatusCode.ToString();
         }
      }

      private bool IsValidEmail(string email)
      {
         // Return true if email is in valid e-mail format.
         return Regex.IsMatch(email, EMAIL_FORMAT);
      }
   }
}
