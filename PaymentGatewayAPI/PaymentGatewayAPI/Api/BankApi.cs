using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Autofac;
using PaymentGatewayAPI.Logger;
using PaymentGatewayAPI.Models;

namespace PaymentGatewayAPI.Api
{
    public class BankApi
    {
        //Get base url from webconfig
        string baseUrl = ConfigurationManager.AppSettings["BankApiUrl"];

        public async Task<BankResponseModel> MakeBankPayment(PaymentBindingModels Payment)
        {
            //build final url
            String url = baseUrl + "api/Bank";

            using (HttpResponseMessage response = await Api.ApiConnector.PostAsJsonAsync(url, Payment))
            {

                BankResponseModel objBankResponse = await response.Content.ReadAsAsync<BankResponseModel>();

                using (var scope = DI.DI.Container.BeginLifetimeScope())
                {
                    var obj = scope.Resolve<ILoggerInterface>();
                    //Logs API call response status
                    string msg = String.Format("STATUS: {0} | URL: {1} | MSG: {2}", objBankResponse.Status, url, objBankResponse.Message);
                    obj.LogMessage("BankApi.MakeBankPayment", msg);
                }

                return objBankResponse;
            }

        }
    }
}