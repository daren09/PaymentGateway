using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PaymentGatewayAPI.Models
{
    public class LogModel
    {
        public int Id { get; set; }
        public string Task { get; set; }
        public string Message { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime LogDate { get; set; }
    }
}