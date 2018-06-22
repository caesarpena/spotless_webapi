using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using MasterWebService.Models;
using MasterWebService.Models.Entities;

namespace MasterWebService.Controllers
{
    public class PostalAreasController : ApiController
    {
        // GET: PostalAreas
        private PostalAreasContext db = new PostalAreasContext();

        // POST: api/PostalAreas/Save
        [ResponseType(typeof(PostalAreas))]
        [Authorize]
        public IHttpActionResult PostArea(PostalAreas postalarea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            db.PostalAreas.Add(postalarea);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { }, postalarea);
        }

        // GET: api/PostalAreas/GetZipCodes
        [ResponseType(typeof(PostalAreas))]
        //[Authorize]
        public IQueryable<PostalAreas> GetZipCodes()
        {

            return db.PostalAreas;
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