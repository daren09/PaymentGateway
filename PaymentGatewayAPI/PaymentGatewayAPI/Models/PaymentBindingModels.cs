using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PaymentGatewayAPI.Models
{
    public class PaymentBindingModels
    {
        [Required]
        [Display(Name = "Card Holder Name")]
        public string CardHolderName { get; set; }

        [Required]
        [Display(Name = "Card Number")]
        [DataType(DataType.CreditCard)]
        public long CardNumber { get; set; }

        [Required]
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        [Required]
        [Display(Name = "CVV")]
        public int Cvv { get; set; }

        [Required]
        [Display(Name = "Card Expiry Date")]
        [StringLength(5, ErrorMessage = "Date should be in MM/YY format", MinimumLength = 5)]
        public String CardExpDate { get; set; }//This needs to be refine to a more strict rules

        [Required]
        [Display(Name = "Currency Code")]
        [StringLength(3, ErrorMessage = "Currency code must be 3 characters", MinimumLength = 3)]
        public String Currency { get; set; }

        /// <summary>
        /// Get a Mask version of the Card Number
        /// </summary>
        /// <returns></returns>
        public string GetMaskCardNumber()
        {
            String pMaskCardNumber = Convert.ToString(this.CardNumber);
            pMaskCardNumber = "XXXX-XXXX-XXXX-" + pMaskCardNumber.Substring(pMaskCardNumber.Length - 4);
            return pMaskCardNumber;
        }
    }
}