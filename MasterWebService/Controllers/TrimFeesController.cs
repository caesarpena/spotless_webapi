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
    public class TrimFeesController : ApiController
    {
        // GET: TrimFees
        private TrimFeesContext db = new TrimFeesContext();

        // GET: api/TrimFees/GetTrimFees
        //[Authorize]
        [ResponseType(typeof(TrimFees))]
        [Route("api/TrimFees/GetTrimFees", Name = "GetTrimFees")]
        public IQueryable<TrimFees> GetVehiclesForCurrentUser()
        {
            var TrimName = Request.Headers.GetValues("Trim").FirstOrDefault();

            return db.TrimFees.Where(s => s.Trim == TrimName);
        }

        // GET: api/TrimFees/GetTrims
        [ResponseType(typeof(TrimFees))]
        [Route("api/TrimFees/GetTrims", Name = "GetTrims")]        
        [Authorize]
        //[System.Web.Http.HttpGet]
        public IQueryable<TrimFees> GetAllTrims()
        {
            Debug.Write("***************************************");

            return db.TrimFees;
        }
    }
}