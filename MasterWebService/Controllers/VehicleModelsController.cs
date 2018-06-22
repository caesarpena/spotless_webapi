using MasterWebService.Models;
using MasterWebService.Models.Entities;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using System.Diagnostics;

namespace MasterWebService.Controllers
{
    public class VehicleModelsController : ApiController
    {
        // GET: VehicleModels
        private ModelContext db = new ModelContext();
        private VehicleContext uvdb = new VehicleContext();


        // GET: api/VehicleModels/GetModels
        [Authorize]
        [Route("api/VehicleModels/GetModels")]
        public IQueryable<VehicleModel> GetVehicles()
        {
            string userId = User.Identity.GetUserId();

            return db.VehicleModel.Where(s => s.ModelId == 1);
        }


     


        // POST: api/VehicleModels/5
        [Route("api/VehicleModels/{id}", Name = "GetMakeModels")]
        public IHttpActionResult PostMakes(Makes make)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var results = db.Database.SqlQuery<_Models>(
                    "Select ModelName " +
                    "From VehicleModels " +
                    "inner join VehicleMake " +
                    "on VehicleMake.MakeId = VehicleModels.MakeId " +
                    "where VehicleMake.MakeName = '"+ make.MakeName+"' "+
                    "GROUP BY ModelName").ToList();

            return CreatedAtRoute("GetMakeModels", new { }, results);

        }




        // POST: api/VehicleModels2/6
        [Route("api/VehicleModels2/{id}", Name = "GetModelsTrims")]
        public IHttpActionResult PostTrims(_Models model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var results = db.Database.SqlQuery<Trims>(
                "Select Trim " +
                "From VehicleModels " +
                "where ModelName = '"+ model.ModelName + "'").ToList();

            return CreatedAtRoute("GetModelsTrims", new {}, results);

        }




        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}