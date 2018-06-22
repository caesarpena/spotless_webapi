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
using System.Collections.Generic;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Formatting;

namespace MasterWebService.Controllers
{
    public class OrdersController : ApiController
    {
        // GET: Orders
        private OrdersContext db = new OrdersContext();

        // GET: api/Orders/GetListofOrders
        [ResponseType(typeof(Orders))]
        [Authorize]
        public IQueryable<Orders> GetListofOrders()
        {
            HttpRequestMessage request = new HttpRequestMessage();

            var values = Request.Headers.GetValues("service_date").FirstOrDefault();

            DateTime serviceDate = Convert.ToDateTime(values);

            return db.Orders.Where(s => s.ServiceDate == serviceDate.Date && !(s.Status.Contains("Completed") || s.Status.Contains("Canceled")));
        }

        // GET: api/Orders/GetFilterAllOrders
        [ResponseType(typeof(Orders))]
        [Authorize]
        [Route("api/Orders/FilterAllOrders", Name = "FilterAllOrders")]
        public IQueryable<Orders> GetFilterAllOrders()
        {
            var statusvalues = Request.Headers.GetValues("status").FirstOrDefault();

            //  var date = Request.Headers.GetValues("service_date").FirstOrDefault();

            // DateTime serviceDate = Convert.ToDateTime(date);

           // Debug.WriteLine("*****************************  " + statusvalues);

            IQueryable<Orders> query;

            if (statusvalues.Contains("On Going"))
            {
                query = db.Orders.Where(o => !(o.Status.Contains("Completed") || o.Status.Contains("Canceled")));
            }
            else
            {
                query = db.Orders.Where(o => o.Status.Contains(statusvalues));
            }

            return query;
        }


        // GET: api/Orders/GetUserCurrentOrders
        [ResponseType(typeof(Orders))]
        [Authorize]
        [Route("api/Orders/UserCurrentOrders", Name = "UserCurrentOrders")]
        public IQueryable<Orders> GetUserCurrentOrders()
        {
            string userId = User.Identity.GetUserId();

            return db.Orders.Where(s => (s.UserId == userId) && !(s.Status.Contains("Completed") || s.Status.Contains("Canceled")));
        }

        // GET: api/Orders/GetUserPastOrders
        [ResponseType(typeof(Orders))]
        [Authorize]
        [Route("api/Orders/UserPastOrders", Name = "UserPastOrders")]
        public IQueryable<Orders> GetUserPastOrders()
        {
            string userId = User.Identity.GetUserId();

            return db.Orders.Where(s => (s.UserId == userId) && (s.Status.Contains("Completed") || s.Status.Contains("Canceled")));
        }

        // GET: api/Orders/OrderDetails
        [ResponseType(typeof(OrdersDetails))]
        [Authorize]
        [Route("api/Orders/OrderDetails", Name = "OrderDetails")]
        public IHttpActionResult GetOrderDetails()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var OrderId = Request.Headers.GetValues("OrderId").FirstOrDefault();

            int oid = Convert.ToInt32(OrderId);

            var OrderModel = db.Orders.Find(oid);

            var results = db.Database.SqlQuery<OrdersDetails>(
                    "Select o.OrderId, anu.FirstName, anu.LastName, anu.PhoneNumber, ca.AddressDesc, ca.City, ca.CityArea, ca.ZipCode, o.AddressId, o.ServiceDate, " +
                    "o.StartTime, o.EndTime, o.Status, o.Total, o.PaymentMethod, o.StripeCreditCardId, o.last4, ps.ServiceName, os.Price, uv.ModelName, uv.Year " +
                    "From Orders as o " +
                    "inner join AspNetUsers as anu " +
                    "on o.UserId = anu.Id " +
                    "inner join (SELECT * FROM CustomerAddress WHERE AddressId = '" + OrderModel.AddressId + "') ca " +
                    "on o.UserId = ca.UserId " +
                    "inner join OrderService as os " +
                    "on o.OrderId = os.OrderId " +
                    "inner join ProductService as ps " +
                    "on os.ServiceId = ps.ServiceId " +
                    "inner join UserVehicles as uv " +
                    "on os.VehicleId = uv.VehicleId " +
                    "where o.OrderId = '" + OrderId + "' ").ToList();

            return CreatedAtRoute("OrderDetails", new { }, results);

        }


        // POST api/Orders/ChangeOrderStatus
        [Authorize]
        [Route("api/Orders/ChangeOrderStatus", Name = "ChangeOrderStatus")]
        public async Task<IHttpActionResult> ChangeOrderStatus([FromBody]FormDataCollection formData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int OrderId = Convert.ToInt32(formData["OrderId"]);
            var Status = formData["Status"];

            var OrderModel = db.Orders.Find(OrderId);

            OrderModel.Status = Status;

            var result = await db.SaveChangesAsync();

            return Ok();
        }

        // POST api/Orders/ChangeOrderStripeCardId
        [Authorize]
        [Route("api/Orders/ChangeOrderStripeCardId", Name = "ChangeOrderStripeCardId")]
        public async Task<IHttpActionResult> ChangeOrderStripeCardId([FromBody]FormDataCollection formData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int OrderId = Convert.ToInt32(formData["OrderId"]);
            var SCardId = formData["StripeCreditCardId"];
            var PM = formData["PaymentMethod"];
            var last4 = formData["last4"];

            var OrderModel = db.Orders.Find(OrderId);

            OrderModel.StripeCreditCardId = SCardId;
            OrderModel.PaymentMethod = PM;
            OrderModel.last4 = last4;

            var result = await db.SaveChangesAsync();

            return Ok();
        }

        // POST: api/Orders/Save
        [ResponseType(typeof(CustomerAddress))]
        [Authorize]
        public IHttpActionResult PostAddress(_Orders orders)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string userId = User.Identity.GetUserId();        

            var ordermodel = new Orders
            {
                UserId = userId,
                AddressId = orders.orderdata.AddressId,
                Status = orders.orderdata.Status,
                Total = orders.orderdata.Total,
                ActualDate = orders.orderdata.ActualDate,
                ServiceDate = orders.orderdata.ServiceDate,
                StartTime = orders.orderdata.StartTime,
                EndTime = orders.orderdata.EndTime,
                PaymentMethod = orders.orderdata.PaymentMethod,
                StripeCreditCardId = orders.orderdata.StripeCreditCardId,
                last4 = orders.orderdata.last4
            };

            using (var db = new OrdersContext())
            {
                db.Orders.Add(ordermodel);
                db.SaveChanges();
            }

            OrderService orderservicemodel;

            for (int i = 0; i < orders.orderservicedata.Count; i++)
            {
                orderservicemodel = new OrderService
                {
                    OrderId = ordermodel.OrderId,
                    ServiceId = orders.orderservicedata[i].ServiceId,
                    Price = orders.orderservicedata[i].ServicePrice,
                    VehicleId = orders.orderservicedata[i].VehicleId
                };

                using (var db = new OrderServiceContext())
                {
                    db.OrdersService.Add(orderservicemodel);
                    db.SaveChanges();
                }
            }
            return CreatedAtRoute("DefaultApi", new { }, orders);
        }
    }
}