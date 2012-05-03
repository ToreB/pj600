using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using log4net;
using no.nith.pj600.dashboard.Code;

namespace no.nith.pj600.dashboard.Admin
{
   public partial class AccountsManagement : System.Web.UI.Page
   {
      private static readonly ILog log = LogManager.GetLogger(typeof(AccountsManagement));

      private const int IS_LOCKED_OUT_INDEX = 4;
      private const int UNLOCK_BUTTON_INDEX = 8;

      protected void Page_Load(object sender, EventArgs e)
      {        
         foreach (GridViewRow row in AccountsList.Rows)
         {
            CheckBox checkbox = (CheckBox)row.Cells[IS_LOCKED_OUT_INDEX].Controls[0];

            //Enables the unlock button if the checkbox is checked
            if (checkbox.Checked)
            {   
               ((LinkButton)row.Cells[UNLOCK_BUTTON_INDEX].FindControl("UnlockButton")).Enabled = true;
            }
         }     
      }

      protected void RowCommand(object sender, GridViewCommandEventArgs e)
      {
         //Finds the row that were activated
         int rowIndex = Convert.ToInt32(e.CommandArgument);
         GridViewRow row = AccountsList.Rows[rowIndex];
         
         //Gets the username of the active row's user
         String username = row.Cells[0].Text;        

         if (e.CommandName.Equals("Unlock"))
         {
            //Unlocks the user's account
            MembershipUser user = Membership.GetUser(username);
            user.UnlockUser();

            log.Info("The user with username '" + username + "' has been unlocked.");
            
            //TODO: Sjekke om mailen blir sendt?
            MailSender.Send(user.Email, "Account Unlocked", "Your account at 99X Dashboard has been unlocked.");
         } 
         else if (e.CommandName.Equals("DeleteUser")) 
         {
            Membership.DeleteUser(username, true);

            log.Info("The user with username '" + username + "' has been deleted.");
         }

         //"Refreshes" the GridView by rebinding the data
         AccountsList.DataBind();
      }
   }
}