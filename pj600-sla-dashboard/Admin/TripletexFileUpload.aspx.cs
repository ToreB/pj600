using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using no.nith.pj600.dashboard.Code;
using no.nith.pj600.dashboard.Code.Exceptions;

namespace no.nith.pj600.dashboard.Admin
{
   public partial class TripletexFileUpload : System.Web.UI.Page
   {
      protected void Page_Load(object sender, EventArgs e)
      {

      }

      /*
       * Method that's called when the Click event is raised from the Button Server Control, FileUploadButton,
       * when the button is clicked.
       * Handles the uploading of the Tripletex export file.
       */
      protected void FileUploadButton_Click(object sender, EventArgs e)
      {

         if (FileUpload.HasFile)
         {
            //Mime types for csv, txt and xls/xlsx files
            string[] allowedMimeTypes = { "text/comma-separated-values", 
                                         "text/csv", "application/csv", 
                                         "application/excel",
                                         "application/vnd.ms-excel",
                                         "application/vnd.msexcel",
                                         "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                         "text/plain"};

            //Check if it's a csv, excel or txt file
            bool valid = false;
            foreach (string type in allowedMimeTypes)
            {
               if (FileUpload.PostedFile.ContentType.Equals(type))
               {
                  valid = true;
                  break;
               }
            }

            if (valid)
            {
               try
               {
                  //Reads the file and write it's content to a table in the database
                  FileHandler.ReadFileAndWriteToDB(FileUpload.PostedFile.InputStream);

                  FileUploadStatusLabel.CssClass = "infoMessage";
                  FileUploadStatusLabel.Text = "File successfully uploaded.";
               }
               catch (TripletexImportException tiEx)
               {
                  FileUploadStatusLabel.CssClass = "errorMessage";
                  FileUploadStatusLabel.Text = tiEx.Message;
               }
            }
            else //Not an valid file type
            {
               FileUploadStatusLabel.CssClass = "errorMessage";
               FileUploadStatusLabel.Text = "The selected file is not a csv, excel or txt file.";
            }
         }
         else //No file selected
         {
            FileUploadStatusLabel.CssClass = "errorMessage";
            FileUploadStatusLabel.Text = "You need to select a file to upload.";
         }
      }
   }
}