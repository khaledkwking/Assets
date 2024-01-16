using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Utilities
{
 
   public   class Logger
    {
       public    string strConLogger { get; set; }

       public enum enumLOGType
       {
           INFORMATION,
           ERROR,
           retry
       }
 

       public Logger(string _strConLogger)
       {
           strConLogger = _strConLogger;
       }





       // inserts a log record in the log table
       public void LogIt(string pcLog, string pcLogType, string ExchangeID)
       {
           if (string.IsNullOrEmpty(pcLog))
           {
               pcLog = "EMPTY";
           }
           pcLog = pcLog.Replace("'", "''");

           using (SqlConnection sqlConnLogger = new SqlConnection(strConLogger))
           {
               sqlConnLogger.Open();

               string strInsert = "INSERT INTO LoggerMatrix (LogMessage, Type,ExchangeID) VALUES ('" + pcLog + "', '" + pcLogType + "','" + ExchangeID + "')";

               using (SqlCommand Command = new SqlCommand(strInsert, sqlConnLogger))
               {
                   try
                   {
                       Command.ExecuteNonQuery();
                   }
                   catch (SqlException ex)
                   {
                   }
               }
           }

       }

       public void LogIt(string pcLog, string pcLogType)
       {
           if (string.IsNullOrEmpty(pcLog))
           {
               pcLog = "EMPTY";
           }
           pcLog = pcLog.Replace("'", "''");
           using (SqlConnection sqlConnLogger = new SqlConnection(strConLogger))
           {
               sqlConnLogger.Open();



               string strInsert = "INSERT INTO LoggerMatrix (LogMessage, Type) VALUES ('" + pcLog + "', '" + pcLogType + "')";

               using (SqlCommand Command = new SqlCommand(strInsert, sqlConnLogger))
               {
                   try
                   {
                       Command.ExecuteNonQuery();
                   }
                   catch (SqlException ex)
                   {
                       //MessageBox.Show("A problem occured in LogIt so program execution will stop.  The following messages will display the values sent to Logit", "Growth", MessageBoxButtons.OK, MessageBoxIcon.Error);
                       //MessageBox.Show("Value of pcLog: " + pcLog);
                       //MessageBox.Show("Value of pcLogType: " + pcLogType);
                       //MessageBox.Show("Sql Server Message: " + ex.Message);
                       //Result = MessageBox.Show("Do you want to continue execution?", "Growth", MessageBoxButtons.YesNo);
                       //if (Result == DialogResult.No)
                       //{
                       //    System.Environment.Exit(0);
                       //}
                   }
               }
           }

          }


       // inserts a log record in the log table
       public void LogUserPage(string strPagePath, string strUserName,string strAction)
       {
           if (string.IsNullOrEmpty(strPagePath))
           {
               strPagePath = "EMPTY";
           }
           strPagePath = strPagePath.Replace("'", "''");

           using (SqlConnection sqlConnLogger = new SqlConnection(strConLogger))
           {
               try
                   {
               sqlConnLogger.Open();

               string strInsert = "INSERT INTO LoggerUser (PagePath, UserName ,Action ) VALUES ('" + strPagePath + "', '" + strUserName + "', '" + strAction + "' )";

               using (SqlCommand Command = new SqlCommand(strInsert, sqlConnLogger))
               {
                  
                       Command.ExecuteNonQuery();
                   
               }
                   }
                   catch (SqlException ex)
                   {
                   }
           }

       }
       // inserts a log Logger Exception in the Logger Exception Table
       public void LoggerException(string strPagePath, string strUserName, string strInnerException, string strMessage, string strStackTrace)
       {
           if (string.IsNullOrEmpty(strPagePath))
           {
               strPagePath = "EMPTY";
           }
           strPagePath = strPagePath.Replace("'", "''");

           using (SqlConnection sqlConnLogger = new SqlConnection(strConLogger))
           {
               try
               {
                   sqlConnLogger.Open();

                   string strInsert = "INSERT INTO LoggerException (PagePath, UserName ,[InnerException] ,[Message] ,[StackTrace] ) VALUES ('"
                       + strPagePath + "', '" + strUserName + "', '" + strInnerException + "', '" + strMessage + "', '" + strStackTrace + "' )";

                   using (SqlCommand Command = new SqlCommand(strInsert, sqlConnLogger))
                   {

                       Command.ExecuteNonQuery();

                   }
               }
               catch (SqlException ex)
               {
               }
           }

       }


       public void Log (string pcLog, string pcLogType)
       {
           if (string.IsNullOrEmpty(pcLog))
           {
               pcLog = "EMPTY";
           }
           pcLog = pcLog.Replace("'", "''");
           using (SqlConnection sqlConnLogger = new SqlConnection(strConLogger))
           {
               sqlConnLogger.Open();



               string strInsert = "INSERT INTO Logger  (LogMessage, Type) VALUES ('" + pcLog + "', '" + pcLogType + "')";

               using (SqlCommand Command = new SqlCommand(strInsert, sqlConnLogger))
               {
                   try
                   {
                       Command.ExecuteNonQuery();
                   }
                   catch (SqlException ex)
                   {
                 
                   }
               }
           }

       }
       public   void LogItMatrix(string pcLog, string pcLogType, string ExchangeID)
       {
           if (string.IsNullOrEmpty(pcLog))
           {
               pcLog = "EMPTY";
           }
           pcLog = pcLog.Replace("'", "''");

           using (SqlConnection sqlConnLogger = new SqlConnection(strConLogger))
           {
               sqlConnLogger.Open();


               string strInsert = "INSERT INTO LoggerMatrix (LogMessage, Type,ExchangeID) VALUES ('" + pcLog + "', '" + pcLogType + "','" + ExchangeID + "')";

               using (SqlCommand Command = new SqlCommand(strInsert, sqlConnLogger))
               {
                   try
                   {
                       Command.ExecuteNonQuery();
                   }
                   catch (SqlException ex)
                   {
                       //MessageBox.Show("A problem occured in LogIt so program execution will stop.  The following messages will display the values sent to Logit", "Growth", MessageBoxButtons.OK, MessageBoxIcon.Error);
                       //MessageBox.Show("Value of pcLog: " + pcLog);
                       //MessageBox.Show("Value of pcLogType: " + pcLogType);
                       //MessageBox.Show("Sql Server Message: " + ex.Message);
                       //Result = MessageBox.Show("Do you want to continue execution?", "Growth", MessageBoxButtons.YesNo);
                       //if (Result == DialogResult.No)
                       //{
                       //    System.Environment.Exit(0);
                       //}
                   }
               }
           }

       }
       public   void LogItMatrix(string pcLog, string pcLogType)
       {
           if (string.IsNullOrEmpty(pcLog))
           {
               pcLog = "EMPTY";
           }
           pcLog = pcLog.Replace("'", "''");

           using (SqlConnection sqlConnLogger = new SqlConnection(strConLogger))
           {
               sqlConnLogger.Open();


               string strInsert = "INSERT INTO LoggerMatrix (LogMessage, Type) VALUES ('" + pcLog + "', '" + pcLogType + "')";

               using (SqlCommand Command = new SqlCommand(strInsert, sqlConnLogger))
               {
                   try
                   {
                       Command.ExecuteNonQuery();
                   }
                   catch (SqlException ex)
                   {
                       //MessageBox.Show("A problem occured in LogIt so program execution will stop.  The following messages will display the values sent to Logit", "Growth", MessageBoxButtons.OK, MessageBoxIcon.Error);
                       //MessageBox.Show("Value of pcLog: " + pcLog);
                       //MessageBox.Show("Value of pcLogType: " + pcLogType);
                       //MessageBox.Show("Sql Server Message: " + ex.Message);
                       //Result = MessageBox.Show("Do you want to continue execution?", "Growth", MessageBoxButtons.YesNo);
                       //if (Result == DialogResult.No)
                       //{
                       //    System.Environment.Exit(0);
                       //}
                   }
               }
           }

       }
 
     }
}
