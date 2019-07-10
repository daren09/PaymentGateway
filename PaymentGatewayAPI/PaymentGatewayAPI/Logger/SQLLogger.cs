using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PaymentGatewayAPI.Models;

namespace PaymentGatewayAPI.Logger
{
    /// <summary>
    /// Stores SQL logs in the database
    /// </summary>
    public class SQLLogger : ILoggerInterface
    {
        public void LogMessage(string Task, string Message)
        {
            using (ApplicationDbContext entities = new ApplicationDbContext())
            {
                //Save log message in the database
                LogModel LogModel = new LogModel()
                {
                    Task = Task,
                    Message = Message,
                    LogDate = DateTime.Now
                };
                entities.Logs.Add(LogModel);
                entities.SaveChanges();
            }
        }
    }
}