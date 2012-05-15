using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using log4net;

namespace no.nith.pj600.dashboard.Code
{
   /**
    * Class that exposes methods for sending email.
    */
   public class MailSender
   {
      private static readonly ILog log = LogManager.GetLogger(typeof(MailSender));

      public static bool Send(string to, string subject, string body)
      {
         //Gets the email stored in the Web.config file in the AppSettings section.
         string from = ConfigurationManager.AppSettings["Email"];
         MailMessage message = new MailMessage(from, to, subject, body);

         SmtpClient client = new SmtpClient();
         client.EnableSsl = true;

         try
         {
            client.Send(message);
            return true;
         } 
         catch(Exception e) 
         {
            log.Error("Something went wrong while trying to send email: " + e.Message);
            return false;
         }
      }
   }
}