using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using no.nith.pj600.dashboard.Code;
using log4net;

namespace no.nith.pj600.dashboard
{
   public partial class Test : System.Web.UI.Page
   {
      private static readonly ILog log = LogManager.GetLogger(typeof(Test));

      private const string xlsMIME = "application/vnd.ms-excel";
      private const string xlsxMIME = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

      protected void Page_Load(object sender, EventArgs e)
      {
         
        DataSet ds = FileHandler.GetDataSetFromExcel("Excel.xlsx");

        if (ds != null)
        {
           GridView1.DataSource = ds.Tables[0].DefaultView;
           GridView1.DataBind();
        }
      }

      protected void ExcelUploadButton_Click(object sender, EventArgs e)
      {

         if (ExcelFileUpload.HasFile)
         {
            if (ExcelFileUpload.PostedFile.ContentType == xlsMIME || ExcelFileUpload.PostedFile.ContentType == xlsxMIME)
            {
               ExcelFileUpload.SaveAs(Server.MapPath("~/App_Data/Excel.xlsx"));

               ExcelUploadStatusLabel.CssClass = "infoMessage";
               ExcelUploadStatusLabel.Text = "File successfully uploaded.";

               log.Info("A new Excel data source has been uploaded.");


               DataSet ds = FileHandler.GetDataSetFromExcel("Excel.xlsx");
               if (ds != null)
               {
                  GridView1.DataSource = ds.Tables[0].DefaultView;
                  GridView1.DataBind();
               }
            }
            else //Not an Excel file
            {
               ExcelUploadStatusLabel.CssClass = "errorMessage";
               ExcelUploadStatusLabel.Text = "The specified file is not an Excel file.";
            }
         }
         else //No file selected
         {
            ExcelUploadStatusLabel.CssClass = "errorMessage";
            ExcelUploadStatusLabel.Text = "You need to specify a file to upload.";
         }
      }
   }
}
