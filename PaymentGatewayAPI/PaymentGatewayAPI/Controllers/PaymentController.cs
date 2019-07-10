using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Autofac;
using PaymentGatewayAPI.Api;
using PaymentGatewayAPI.Logger;
using PaymentGatewayAPI.Models;

namespace PaymentGatewayAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/Payment")]
    public class PaymentController : ApiController
    {

        // GET Payment details
        public HttpResponseMessage Get(int id)
        {
            try
            {
                using (ApplicationDbContext entities = new ApplicationDbContext())
                {
                    //Retrieve payment details
                    PaymentModel objPayment = entities.Payments.FirstOrDefault(x => x.Id == id);

                    if (objPayment == null)
                        throw new Exception("Payment info not found");

                    var objResponse = new
                    {
                        status = "OK",
                        CardHolderName = objPayment.CardHolderName,
                        CardNumber = objPayment.CardNumber,
                        CardExpDate = objPayment.CardExpDate,
                        RequestDate = objPayment.RequestDate,
                        Currency = objPayment.Currency,
                        BankStatus = objPayment.BankStatus,
                        BankIdentifer = objPayment.BankIdentifer
                    };

                    var PaymentDetails = Request.CreateResponse(HttpStatusCode.Created, objResponse);

                    return PaymentDetails;
                }
            }
            catch (Exception ex)
            {

                using (var scope = DI.DI.Container.BeginLifetimeScope())
                {
                    var obj = scope.Resolve<ILoggerInterface>();
                    //Logger error Message
                    obj.LogMessage("PaymentsController.Get", ex.Message);
                }

                ErrorResponseModel objResponse = new ErrorResponseModel() { Status = "ERROR", Message = ex.Message };
                return Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
            }
        }

        // POST: Make a payment
        public async Task<HttpResponseMessage> Post([FromBody] PaymentBindingModels Payment)
        {
            try
            {
                //In case POST send with no data
                if (Payment == null)
                    throw new Exception("No data provided");

                if (!ModelState.IsValid)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);


                using (ApplicationDbContext entities = new ApplicationDbContext())
                {
                    //Retrieve Authenticated User
                    var AuthenticatedUser = entities.Users.FirstOrDefault(x => x.Email == User.Identity.Name);

                    if (AuthenticatedUser == null)
                        throw new Exception("Unable to retrieve authenticated user");


                    //Request Bank Payment
                    BankApi objBank = new BankApi();
                    BankResponseModel objresponse = await objBank.MakeBankPayment(Payment);


                    PaymentModel PayModel = new PaymentModel()
                    {
                        CardHolderName = Payment.CardHolderName,
                        CardNumber = Payment.GetMaskCardNumber(),
                        Amount = Payment.Amount,
                        Currency = Payment.Currency.ToUpper(),
                        AuthUser = AuthenticatedUser,
                        CardExpDate = Payment.CardExpDate,
                        Cvv = Payment.Cvv,
                        RequestDate = DateTime.Now,
                        BankIdentifer = objresponse.Identifier,
                        BankStatus = objresponse.Status
                    };

                    //Save Payment
                    entities.Payments.Add(PayModel);
                    entities.SaveChanges();

                    if (objresponse.Status != "OK")
                        throw new Exception("Error processing payment with bank");

                    var objPayResponse = new
                    {
                        status = "OK",
                        PaymentBankIdentifer = PayModel.BankIdentifer,
                        GetPayment = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/api/Payment/" + PayModel.Id
                    };

                    var messsage = Request.CreateResponse(HttpStatusCode.Created, objPayResponse);

                    return messsage;

                }


            }
            catch (Exception ex)
            {

                using (var scope = DI.DI.Container.BeginLifetimeScope())
                {
                    var obj = scope.Resolve<ILoggerInterface>();
                    //Logger error Message
                    obj.LogMessage("PaymentsController.Post", ex.Message);
                }

                ErrorResponseModel objResponse = new ErrorResponseModel() { Status = "ERROR", Message = ex.Message };
                return Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
            }
        }


    }
}
