using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using log4net;

namespace no.nith.pj600.dashboard.Admin
{
   public partial class AccountsManagement : System.Web.UI.Page
   {
      private static readonly ILog log = LogManager.GetLogger(typeof(AccountsManagement));

      private const int IS_LOCKED_OUT_INDEX = 3;
      private const int UNLOCK_BUTTON_INDEX = 6;

      protected void Page_Load(object sender, EventArgs e)
      {        
         foreach (GridViewRow row in AccountsList.Rows)
         {
            //Cell no. 4 is be the IsLockedOut checkboxfield
            CheckBox checkbox = (CheckBox)row.Cells[IS_LOCKED_OUT_INDEX].Controls[0];

            //Makes the button in the rows with a non-checked checkbox invisible
            if (!checkbox.Checked)
            {  
               //Cell no. 7 is the buttonfield
               row.Cells[UNLOCK_BUTTON_INDEX].Controls[0].Visible = false;
            }
         }     
      }

      protected void RowCommand(object sender, GridViewCommandEventArgs e)
      {
         if (e.CommandName.Equals("Unlock"))
         {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = AccountsList.Rows[rowIndex];

            String username = row.Cells[0].Text;
            MembershipUser user = Membership.GetUser(username);
            user.UnlockUser();

            //row.Cells[UNLOCK_BUTTON_INDEX].Controls[0].Visible = false;
            AccountsList.DataBind();

            log.Info("The user with username '" + username + "' has been unlocked.");
         }
      }
   }
}