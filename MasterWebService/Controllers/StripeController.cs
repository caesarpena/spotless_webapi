using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using MasterWebService.Models;
using MasterWebService.Models.Entities;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using Stripe;

namespace MasterWebService.Controllers
{
    public class StripeController : ApiController
    {
        ApplicationDbContext userdb = new ApplicationDbContext();
        OrdersContext odb = new OrdersContext();

        //string StripeKey = "sk_test_IDCWmdnNzkM8Hx7o9k1ShN8Y";
        string StripeKey = "sk_live_19kfyEQWmtCWGO4KLhUJHHWb";

        // GET: Stripe

        // POST: api/Stripe/CreateCustomer
        [Authorize]
        [Route("api/Stripe/CreateCustomer", Name = "CreateCustomer")]
        public  async Task<IHttpActionResult> StripeCreateCustomer(CreateStripeCustomer data)
        {
            try
            {
                StripeConfiguration.SetApiKey(StripeKey);

                string userId = User.Identity.GetUserId();

                var UserModel = userdb.Users.Find(userId);

                //Ana es loca

                var stripeTokenCreateOptions = new StripeTokenCreateOptions
                {
                    Card = new StripeCreditCardOptions
                    {
                        Number = data.Number,
                        ExpirationMonth = Convert.ToInt32(data.ExpiryMonth),
                        ExpirationYear = Convert.ToInt32(data.ExpiryYear),
                        Cvc = data.CVC,
                        Name = data.Name
                    }
                };

                var tokenService = new StripeTokenService();
                var stripeToken = tokenService.Create(stripeTokenCreateOptions);
                var token = stripeToken.Id;


                var customerOptions = new StripeCustomerCreateOptions()
                {
                    Description = "Customer for "+ UserModel.Email,
                    SourceToken = token
                };

                var customerService = new StripeCustomerService();
                StripeCustomer customer = customerService.Create(customerOptions);

                UserModel.StripeCustomerId = customer.Id;

                var result = await userdb.SaveChangesAsync();

            }
            catch(Exception sex)
            {
                return Content(HttpStatusCode.BadRequest, sex);
            }          
        
            return Ok();
        }


        // POST: api/Stripe/ChargeOrder
        [Authorize]
        [Route("api/Stripe/ChargeOrder", Name = "ChargeOrder")]
        public async Task<IHttpActionResult> StripeChargeOrder(StripeChargeOrder data)
        {
            StripeConfiguration.SetApiKey(StripeKey);
            IHttpActionResult a = Ok();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string userId = User.Identity.GetUserId();

                var query = (from u in userdb.Users
                             where u.Id == userId
                             select new
                             {
                                 u.StripeCustomerId,
                             }).First();

                var StripeID = query.StripeCustomerId;               

                var chargeOptions = new StripeChargeCreateOptions()
                {
                    Amount = data.amount,
                    Currency = "usd",
                    Description = data.description+" "+data.orderid,
                    CustomerId = StripeID,
                    SourceTokenOrExistingSourceId = data.card
                };

                var chargeService = new StripeChargeService();
                StripeCharge charge = chargeService.Create(chargeOptions);

                int OrderId = Convert.ToInt32(data.orderid);

                var OrderModel = odb.Orders.Find(OrderId);

                OrderModel.Status = data.status;

                var result = await odb.SaveChangesAsync();
            }
            catch (Exception sex)
            {
                return Content(HttpStatusCode.BadRequest, sex);
            }
            return Ok();
        }


        // POST: api/Stripe/AddCardToCustomer
        [Authorize]
        [Route("api/Stripe/AddCardToCustomer", Name = "AddCardToCustomer")]
      /*  public IHttpActionResult StripeNewCardToCustomerAsync(CreateStripeCustomer data)
        {
            StripeConfiguration.SetApiKey(StripeKey);
            IHttpActionResult a = Ok();
            try
            {
                string userId = User.Identity.GetUserId();

                var query = (from u in userdb.Users
                             where u.Id == userId
                             select new
                             {
                                 u.StripeCustomerId,
                             }).First();

                var StripeID = query.StripeCustomerId;


                var cardOptions = new StripeCardCreateOptions()
                {
                    SourceToken = data.token
                };

                var cardService = new StripeCardService();

                StripeCard card = cardService.Create(StripeID, cardOptions);
            }
            catch (Exception sex) {

                return Content(HttpStatusCode.BadRequest, sex);
            }
           
            return Ok();
        }


        // POST: api/Stripe/DeleteCustomerCard
        [Authorize]
        [Route("api/Stripe/DeleteCustomerCard", Name = "DeleteCustomerCard")]
        public IHttpActionResult StripeDeleteCustomerCardAsync([FromBody]FormDataCollection formData)
        {
            StripeConfiguration.SetApiKey(StripeKey);

            var cardid = formData["cardid"];

            try
            {
                string userId = User.Identity.GetUserId();

                var query = (from u in userdb.Users
                             where u.Id == userId
                             select new
                             {
                                 u.StripeCustomerId,
                             }).First();

                var StripeID = query.StripeCustomerId;

                var cardService = new StripeCardService();

                StripeDeleted card = cardService.Delete(StripeID, cardid);
            }

            catch (Exception sex)
            {
                return Content(HttpStatusCode.BadRequest, sex);
            }
            return Ok();
        }*/

        

        //create card from server side
        /*  var myCustomer = new StripeCardCreateOptions();
             myCustomer.SourceCard = new SourceCard
             {
                 Number = txtnumero.Text,
                ExpirationYear = txtyear.Text,
                ExpirationMonth = txtmonth.Text,
                 Cvc = txtCVV.Text
             };*/


        // GET: api/Stripe/GetCustomerCards
        [Authorize]
        [Route("api/Stripe/GetCustomerCards", Name = "GetCustomerCards")]
        public IHttpActionResult GetCustomerCards()
        {
            StripeConfiguration.SetApiKey(StripeKey);
            IEnumerable<StripeCard> response;
            string userId = User.Identity.GetUserId();

            var query = (from u in userdb.Users
                         where u.Id == userId
                         select new
                         {
                             u.StripeCustomerId,
                         }).First();

            var StripeID = query.StripeCustomerId;

            try
            {
                var cardService = new StripeCardService();
                response = cardService.List(StripeID);
            }
            catch (StripeException sex) {

                return Content(HttpStatusCode.BadRequest, sex);
            }
            return CreatedAtRoute("GetCustomerCards", new { }, response.ToList());
        }
    }
}