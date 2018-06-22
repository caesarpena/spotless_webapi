using MasterWebService.Models;
using MasterWebService.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace MasterWebService.Controllers
{
    public class OrderStatusController : ApiController
    {
        // GET: OrderStatus
        private OrderStatusContext db = new OrderStatusContext();

        // GET: api/OrderStatus/GetStatus
        [ResponseType(typeof(OrderStatus))]
        [Authorize]
        public IQueryable<OrderStatus> GetStatus()
        {
            return db.Status;
        }
    }
}