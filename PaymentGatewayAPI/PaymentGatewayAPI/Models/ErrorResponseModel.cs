﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaymentGatewayAPI.Models
{
    public class ErrorResponseModel
    {
        public string Status { get; set; }
        public string Message { get; set; }
    }
}