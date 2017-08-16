using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using webAPISecSess.Models;

namespace webAPISecSess.Controllers
{
    [Authorize]
    public class TouristPlacesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/TouristPlaces
        public IEnumerable<TouristPlace> GetTouristPlaceSet()
        {
            return db.TouristPlaceSet.ToList();
        }

        // GET: api/TouristPlaces/5
        [ResponseType(typeof(TouristPlace))]
        public IHttpActionResult GetTouristPlace(int id)
        {
            TouristPlace touristPlace = db.TouristPlaceSet.Find(id);
            if (touristPlace == null)
            {
                return NotFound();
            }

            return Ok(touristPlace);
        }

        // PUT: api/TouristPlaces/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTouristPlace(int id, TouristPlace touristPlace)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != touristPlace.Id_TouristPlace)
            {
                return BadRequest();
            }
            

            try
            {
                db.Entry(touristPlace).State = EntityState.Modified;
                db.SaveChanges();
            }

            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var clientValues = (TouristPlace)entry.Entity;
                var databaseEntry = entry.GetDatabaseValues();
                if (databaseEntry == null)
                {
                    ModelState.AddModelError(string.Empty,
                        "Unable to save changes. The entity was deleted by another user.");
                    return BadRequest(ModelState);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Unable to save changes. The entity was updated by another user.");
                    return BadRequest(ModelState);
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/TouristPlaces
        [ResponseType(typeof(TouristPlace))]
        public IHttpActionResult PostTouristPlace(TouristPlace touristPlace)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TouristPlaceSet.Add(touristPlace);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = touristPlace.Id_TouristPlace }, touristPlace);
        }

        // DELETE: api/TouristPlaces/5
       
        [ResponseType(typeof(TouristPlace))]
        public IHttpActionResult DeleteTouristPlace(int id)
        {
            TouristPlace touristPlace = db.TouristPlaceSet.Find(id);
            if (touristPlace == null)
            {
                return NotFound();
            }

            db.TouristPlaceSet.Remove(touristPlace);
            db.SaveChanges();

            return Ok(touristPlace);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TouristPlaceExists(int id)
        {
            return db.TouristPlaceSet.Count(e => e.Id_TouristPlace == id) > 0;
        }
    }
}