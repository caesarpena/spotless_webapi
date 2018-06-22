using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using MasterWebService.Models;
using MasterWebService.Models.Entities;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System;

namespace MasterWebService.Controllers
{
    public class VehicleController : ApiController
    {
        // GET: Vehicle
        private VehicleContext db = new VehicleContext();
       // private MakeContext VehicleMakedb = new MakeContext();
       // private ModelContext VehicleModeldb = new ModelContext();


        // GET: api/Vehicle/GetUserListofVehicles
        //[Authorize]
        [ResponseType(typeof(UserVehicles))]
        public IQueryable<UserVehicles> GetVehiclesForCurrentUser()
        {
            string userId = User.Identity.GetUserId();
            return db.Vehicle.Where(s => s.UserId == userId && s.Deleted!=true);
        }


        // POST: api/Vehicle/SaveUserVehicle
        [ResponseType(typeof(UserVehicles))]
            [Authorize]
            public IHttpActionResult PostUserVehicle(UserVehicles Vehicle)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                string userId = User.Identity.GetUserId();
                Vehicle.UserId = userId;

                    db.Vehicle.Add(Vehicle);
                    db.SaveChanges();

                return CreatedAtRoute("DefaultApi", new { }, Vehicle);
            }


        // POST: api/Vehicle/DeleteV
        [ResponseType(typeof(UserVehicles))]
        [Authorize]
        [Route("api/Vehicle/DeleteV", Name = "DeleteV")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult DeleteVehicle(UserVehicles vehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var Model = db.Vehicle.Find(vehicle.VehicleId);

                if (Model != null)
                {
                    Model.Deleted = true;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
            return Ok();
        }






        // POST: api/Vehicle/SaveMake
        /*    [ResponseType(typeof(VehicleMake))]
           // [Authorize]
            public IHttpActionResult PostMake(VehicleMake vehiclemake)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                VehicleMakedb.VehicleMake.Add(vehiclemake);
                VehicleMakedb.SaveChanges();

                return CreatedAtRoute("DefaultApi", new { }, vehiclemake);
            }*/

        // POST: api/Vehicle/SaveModel


        /* [ResponseType(typeof(VehicleModel))]
         // [Authorize]
         public IHttpActionResult PostModel(VehicleModel vehiclemodel)
         {
             if (!ModelState.IsValid)
             {
                 return BadRequest(ModelState);
             }

             VehicleModeldb.VehicleModel.Add(vehiclemodel);
             VehicleModeldb.SaveChanges();

             return CreatedAtRoute("DefaultApi", new { }, vehiclemodel);
         }*/



        /*  // GET: api/Vehicle/GetVehicles
          [ResponseType(typeof(PostalAreas))]
          //[Authorize]
          public IQueryable<PostalAreas> GetZipCodes()
          {
              return db.PostalAreas;
          }
          */

    }
}



/*  var vehicles = (from UserVehicles in UserVehicledb.Vehicle
                            join VehicleMakes in VehicleMakedb.VehicleMake
                            on UserVehicles.MakeId equals VehicleMakes.MakeId
                            join VehicleModels in VehicleModeldb.VehicleModel
                            on VehicleMakes.MakeId equals VehicleModels.MakeId
                            where (UserVehicles.UserId == userId)
                            select new
                            {
                               MakeName= VehicleMakes.MakeName,
                               MakeModel = VehicleModels.ModelName,
                               Trim =  VehicleModels.Trim,
                               Year = VehicleModels.Year
                            }).ToList();

             foreach (var v in vehicles)
             {
                 UserVehiclesDetails o = new UserVehiclesDetails();
                 o.MakeName = v.MakeName;
                 o.ModelName = v.MakeModel;
                 o.Trim = v.Trim;
                 o.Year = v.Year;
                 abc.Add(o);
             }*/

// return abc;

// vehicles.ForEach(i => i. = i.Description);


/*IQueryable<UserVehiclesDetails> _vehicles =
    from c in UserVehicledb.Vehicle
    from p in VehicleMakedb.VehicleMake
    from s in VehicleModeldb.VehicleModel
    where p.MakeId == c.MakeId
    where s.MakeId == p.MakeId
    where c.UserId == userId
    select new UserVehiclesDetails
    {
        MakeName = p.MakeName,
        ModelName = s.ModelName,
        Trim = s.Trim,
        Year = s.Year

};*/
