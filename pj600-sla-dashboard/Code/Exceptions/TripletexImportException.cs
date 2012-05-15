using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace no.nith.pj600.dashboard.Code.Exceptions
{
   /**
    * <summary>
    * Exception thrown when something goes wrong when importing data from Tripletex export file.
    * </summary>
    */
   public class TripletexImportException : Exception
   {
      public TripletexImportException(string message)
         : base("Something went wrong while importing data from Tripletex export file: " + message)
      {
         
      }
   }
}