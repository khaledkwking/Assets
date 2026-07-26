using Infrastructure.DAL.Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.Web.Helper
{
    public static class Logger
    {
        public static void Log(string userId, string userName, string tableName, string action, string recordId = null)
        {
            try
            {
                using (var db = new AssetsEntitiesNew()) // Replace with your actual context
                {
                    var log = new SystemLog
                    {
                        UserId = userId ?? "Unknown",
                        UserName = userName ?? "Unknown",
                        TableName = tableName ?? "Unknown",
                        RecordId = recordId ?? "",
                        Action = action ?? "Unknown Action",
                        Page = HttpContext.Current?.Request?.RawUrl ?? "Unknown",
                        IPAddress = HttpContext.Current?.Request?.UserHostAddress ?? "Unknown",
                        AdditionalInfo = "",
                        Timestamp = DateTime.Now
                    };

                    db.SystemLogs.Add(log);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                // Optional: write to file or event log
            }
        }
    }

}