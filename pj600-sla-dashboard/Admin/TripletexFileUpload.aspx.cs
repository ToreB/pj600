﻿using System;
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

      protected void FileUploadButton_Click(object sender, EventArgs e)
      {

         if (FileUpload.HasFile)
         {

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
                  FileHandler.ReadCSVAndWriteToDB(FileUpload.PostedFile.InputStream);

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