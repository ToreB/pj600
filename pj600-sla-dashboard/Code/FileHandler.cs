using System;
using System.Data;
using System.Data.OleDb;

using log4net;
using System.IO;
using no.nith.pj600.dashboard.Code.Exceptions;
using System.Configuration;


namespace no.nith.pj600.dashboard.Code
{

   /**
    * <summary>
    * Class that handles reading from files.
    * </summary>
    */
   public class FileHandler
   {
      private static readonly ILog log = LogManager.GetLogger(typeof(FileHandler));

      /**
       * <summary>
       * Method that reads a Tripletex export file and write it's content to a table in the database.
       * </summary>
       */
      public static void ReadFileAndWriteToDB(Stream stream)
      {        
         StreamReader reader = new StreamReader(stream);
         DatabaseClassesDataContext db = new DatabaseClassesDataContext(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);

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