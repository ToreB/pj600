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

      protected void FileUploadButton_Click(object sender, EventArgs e) 
      {

         if (FileUpload.HasFile)
         {

            /*string[] CSV_MIME = { "text/comma-separated-values", 
                                         "text/csv", "application/csv", 
                                         "application/excel",
                                         "application/vnd.ms-excel",
                                         "application/vnd.msexcel" };

            //Check if it's a csv file
            bool valid = false;
            foreach (string mime in CSV_MIME)
            {
               if (ExcelFileUpload.PostedFile.ContentType.Equals(mime))
               {
                  valid = true;
                  break;
               }
            }*/

            //if (valid)
            //{
               //ExcelFileUpload.SaveAs(Server.MapPath("~/App_Data/Excel.xlsx"));
               try
               {
                  FileHandler.ReadCSVAndWriteToDB(FileUpload.PostedFile.InputStream);

                  FileUploadStatusLabel.CssClass = "infoMessage";
                  FileUploadStatusLabel.Text = "File successfully uploaded.";
               }
               catch (TripletexImportException tiEx)
               {
                  FileUploadStatusLabel.CssClass = "errorMessage";
                  FileUploadStatusLabel.Text = tiEx.Message;
               }
               

               //log.Info("A new Excel data source has been uploaded.");
            //}
            /*else //Not an valid file
            {
               ExcelUploadStatusLabel.CssClass = "errorMessage";
               ExcelUploadStatusLabel.Text = "The specified file is not an Excel file.";
            }*/
         }
         else //No file selected
         {
            FileUploadStatusLabel.CssClass = "errorMessage";
            FileUploadStatusLabel.Text = "You need to specify a file to upload.";
         }
      }
   }
}