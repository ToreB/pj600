using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace no.nith.pj600.dashboard.Code.Exceptions
{
   public class TripletexImportException : Exception
   {
      public TripletexImportException(string message)
         : base("Something went wrong while importing data from Tripletex export file: " + message)
      {
         
      }
   }
}