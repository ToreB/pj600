using System;
using System.Data;
using System.Data.OleDb;

using log4net;
using System.IO;
using no.nith.pj600.dashboard.Code.Exceptions;


namespace no.nith.pj600.dashboard.Code
{

   public class FileHandler
   {
      private static readonly ILog log = LogManager.GetLogger(typeof(FileHandler));

      public const string XLS_MIME = "application/vnd.ms-excel";
      public const string XLSX_MIME = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

      /**<summary>
       * Gets a DataSet for the specified Excel document.
       * </summary>
       */
      public static DataSet GetDataSetFromExcel(string fileName)
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

      public static void ReadCSVAndWriteToDB(Stream stream)
      {        
         StreamReader reader = new StreamReader(stream);
         DatabaseClassesDataContext db = new DatabaseClassesDataContext();

         string line;
         while((line = reader.ReadLine()) != null) 
         {
            try
            {
               line = line.Substring(1, line.Length-2); //Removes the surrounding ""
               string[] columns = line.Split(';');
            
               TripletexImport row = new TripletexImport
               {
                  ProjectNo = int.Parse(columns[0]),
                  ProjectName = columns[1],
                  ProjectLeader = columns[2],
                  DepName = columns[3],
                  EmployeeName = columns[4],
                  Date = DateTime.Parse(columns[5]),
                  Hours = double.Parse(columns[6]),
                  Comment = columns[7]
               };

               db.TripletexImports.InsertOnSubmit(row);
            }
            catch (Exception e)
            {
               TripletexImportException tiEx = new TripletexImportException(e.Message);
               log.Error(tiEx.Message);
               throw tiEx;
            }
         }

         try
         {
            //Deletes the current content
            db.ExecuteCommand("DELETE FROM TripletexImport");

            //Submits the new content
            db.SubmitChanges();

            log.Info("A new CSV file was read and TripletexImport table was updated.");
         }
         catch (Exception e)
         {
            log.Error("Something went wrong while trying to update the TripletexImport table: " + e.Message);

         }
      }

   }
}