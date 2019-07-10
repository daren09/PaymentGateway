using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BankAPI.Models;

namespace BankAPI.Controllers
{
    public class BankController : ApiController
    {

        // POST: api/Bank
        public HttpResponseMessage Post([FromBody]string value)
        {
            try
            {
                //Simluate an error for testing
                //Random objRandom = new Random();
                //int num = objRandom.Next(1, 3);
                //if (num == 2)
                //   throw new Exception("Simulated error");

                BankResponseModel objBankResponse = new BankResponseModel() {   Identifier = Guid.NewGuid().ToString(),
                                                                                Status = "OK",
                                                                                Message ="Payment Process Successfully" };

                return Request.CreateResponse(HttpStatusCode.OK, objBankResponse);
            }
            catch (Exception ex)
            {
                BankResponseModel objBankErrorResponse = new BankResponseModel() { Status = "ERROR", Message = ex.Message };
                return Request.CreateResponse(HttpStatusCode.InternalServerError, objBankErrorResponse);
            }
        }
    
    }
}
