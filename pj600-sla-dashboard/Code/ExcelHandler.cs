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
         string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\" + fileName + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1;\"";
         OleDbConnection con = new OleDbConnection(connectionString);
         DataSet dataSet = null;

         try
         {
            con.Open();

            OleDbCommand cmd = new OleDbCommand("select * from [Ark1$]", con);
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            adapter.SelectCommand = cmd;

            dataSet = new DataSet();
            adapter.Fill(dataSet, "Test");
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