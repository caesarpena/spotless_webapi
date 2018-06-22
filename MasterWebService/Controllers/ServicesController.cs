using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using MasterWebService.Models;
using MasterWebService.Models.Entities;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace MasterWebService.Controllers
{
    public class ServicesController : ApiController
    {
        private ServicesContext db = new ServicesContext();


        // GET: api/Services/GetServices
        [ResponseType(typeof(ServiceInfo))]
        [Route("api/Services/GetServices", Name = "GetServices")]
        [Authorize]
        public IHttpActionResult GetServices()
        {
            var FeeId = Request.Headers.GetValues("Feeid").FirstOrDefault();

            var results = db.Database.SqlQuery<ServiceInfo>(
                   "Select ServiceId, ServiceName, ServiceDescription, Time, Price, (SELECT Fees FROM TrimFees WHERE FeeId = '"+ FeeId + "') Fee " +
                   "From ProductService").ToList();

            return CreatedAtRoute("GetServices", new { }, results);
        }
    }
}