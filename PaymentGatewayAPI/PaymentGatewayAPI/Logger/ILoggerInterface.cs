using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaymentGatewayAPI.Logger
{
    public interface ILoggerInterface
    {
      void LogMessage(string Task, string Message);
    }
}