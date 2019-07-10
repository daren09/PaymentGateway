using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace PaymentGatewayAPI.Api
{
    public class Api
    {
        public static HttpClient ApiConnector { get; set; }

        public static void InitializeApiConnection()
        {
            ApiConnector = new HttpClient();
            ApiConnector.DefaultRequestHeaders.Accept.Clear();
            ApiConnector.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}