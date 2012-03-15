using System;
using System.Data;
using System.Data.OleDb;

using log4net;


namespace no.nith.pj600.dashboard.Code
{

   public class ExcelHandler
   {
      private static readonly ILog log = LogManager.GetLogger(typeof(ExcelHandler));

      /**<summary>
       * Gets a DataSet for the specified Excel document.
       * </summary>
       */
      public static DataSet GetDataSet(string fileName)
      {
         /*
            "HDR=Yes;" indicates that the first row contains columnnames, not data. "HDR=No;" indicates the opposite.
            "IMEX=1;" tells the driver to always read "intermixed" (numbers, dates, strings etc) data columns as text. Note that this option might affect excel sheet write access negative. 
          */
         string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\" + fileName + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1;\"";
         OleDbConnection con = new OleDbConnection(connectionString);
         DataSet dataSet = null;

         try
         {
            con.Open();

            //Gets the name of the first sheet in the Excel document
            DataTable dataTable = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string sheetName = dataTable.Rows[0]["TABLE_NAME"].ToString();

            //Sets the select command
            OleDbCommand cmd = new OleDbCommand("select * from [" + sheetName + "]", con);
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            adapter.SelectCommand = cmd;

            //Fills a DataSet with the data from the Excel document
            dataSet = new DataSet();
            adapter.Fill(dataSet);
         }
         catch (Exception ex)
         {
            log.Error("Something went wrong while accessing " + fileName + ": " + ex.Message);
         }
         finally
         {
            con.Close();
         }

         return dataSet;
      }

   }
}