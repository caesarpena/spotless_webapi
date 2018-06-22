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
    public class ServiceUpgradeController : ApiController
    {
        private ServiceUpgradeContext db = new ServiceUpgradeContext();


        // GET: api/ServiceUpgrade/GetUpgrades
        [ResponseType(typeof(ServiceUpgrades))]
        //[Authorize]
        public IQueryable<ServiceUpgrades> GetUpgrades()
        {
            return db.Upgrade;
        }
    }
}