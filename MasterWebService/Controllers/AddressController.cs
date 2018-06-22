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

namespace MasterWebService.Controllers
{
    public class AddressController : ApiController
    {
        // GET: Address
        private AddressContext db = new AddressContext();





        // GET: api/Address/GetUserListofAddress
        [Authorize]
        [Route("api/Address/GetUserListofAddress", Name = "GetUserListofAddress")]
        public IQueryable<CustomerAddress> GetAddressForCurrentUser()
        {
            string userId = User.Identity.GetUserId();

            return db.Address.Where(s => s.UserId == userId && s.Deleted!=true);
        }



        // POST: api/Address/Save
        [ResponseType(typeof(CustomerAddress))]
        [Authorize]
        [Route("api/Address/Save", Name = "Save")]
        public IHttpActionResult PostAddress(CustomerAddress addressmodel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string userId = User.Identity.GetUserId();

          //  Debug.WriteLine("*****************************" + userId);

            addressmodel.Deleted = false;
            addressmodel.UserId = userId;

            db.Address.Add(addressmodel);

            db.SaveChanges();
            return CreatedAtRoute("Save", new { /*id = addressmodel.AddressId */}, addressmodel);
        }


        // POST: api/Address/Delete
        [ResponseType(typeof(CustomerAddress))]
        [Authorize]
        [Route("api/Address/Delete", Name = "Delete")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult DeleteAddress(CustomerAddress addressmodel)
        {
            //addressmodel.Deleted = true;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var Model = db.Address.Find(addressmodel.AddressId);

                if (Model != null)
                {
                    Model.Deleted = true;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("********************************** "+ex.InnerException);
                return Content(HttpStatusCode.BadRequest, ex);
            }
            return Ok();
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