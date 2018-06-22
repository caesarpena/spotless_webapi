using MasterWebService.Models;
using MasterWebService.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;

namespace MasterWebService.Controllers
{
    public class VehicleMakesController : ApiController
    {
        // GET: Make
        private MakeContext db = new MakeContext();

        // GET: api/VehicleMakes/GetMakes
        [ResponseType(typeof(VehicleMake))]
        //[Authorize]
        public IQueryable<VehicleMake> GetMakes()
        {
            return db.VehicleMake;
        }
    }
}