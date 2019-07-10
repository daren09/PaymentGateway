using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PaymentGatewayAPI.Models
{
    public class PaymentModel
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public string CardExpDate { get; set; }//This need to be store in a different way to faciliate reporting needs.
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public int Cvv { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime RequestDate { get; set; }
        public string BankStatus { get; set; }
        public string BankIdentifer { get; set; }

        public virtual ApplicationUser AuthUser { get; set; }
    }
}