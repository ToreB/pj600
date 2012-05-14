using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace no.nith.pj600.dashboard.Code
{
   public struct SlaTableStruct
   {
      private int projectNo;
      private string projectName;
      private string customerName;
      private string projectManager;

      public int ProjectNo
      {
         get { return projectNo; }
         set { projectNo = value; }
      }

      public string ProjectName
      {
         get { return projectName; }
         set { projectName = value; }
      }

      public string CustomerName
      {
         get { return customerName; }
         set { customerName = value; }
      }

      public string ProjectManager
      {
         get { return projectManager; }
         set { projectManager = value; }
      }
   }
}