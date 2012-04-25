using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using log4net;
using no.nith.pj600.dashboard.Code;
using no.nith.pj600.dashboard.Code.Exceptions;

namespace no.nith.pj600.dashboard.Admin
{
   public partial class AdminPanel : System.Web.UI.Page
   {
      private static readonly ILog log = LogManager.GetLogger(typeof(AdminPanel));

      protected void Page_Load(object sender, EventArgs e)
      {

      }
   }
}