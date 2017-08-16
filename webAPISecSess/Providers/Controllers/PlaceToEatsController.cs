using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
    public class PlaceToEatsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/PlaceToEats
        public IEnumerable<PlaceToEat> GetPlacesToEatSet()
        {
            return db.PlacesToEatSet.ToList();
        }

        // GET: api/PlaceToEats/5
        [ResponseType(typeof(PlaceToEat))]
        public IHttpActionResult GetPlaceToEat(int id)
        {
            PlaceToEat placeToEat = db.PlacesToEatSet.Find(id);
            if (placeToEat == null)
            {
                return NotFound();
            }

            return Ok(placeToEat);
        }

        // PUT: api/PlaceToEats/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPlaceToEat(int id, PlaceToEat placeToEat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != placeToEat.Id_PlaceToEat)
            {
                return BadRequest();
            }

            db.Entry(placeToEat).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlaceToEatExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PlaceToEats
        [ResponseType(typeof(PlaceToEat))]
        public IHttpActionResult PostPlaceToEat(PlaceToEat placeToEat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PlacesToEatSet.Add(placeToEat);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = placeToEat.Id_PlaceToEat }, placeToEat);
        }

        // DELETE: api/PlaceToEats/5
        
        [ResponseType(typeof(PlaceToEat))]
        public IHttpActionResult DeletePlaceToEat(int id)
        {
            PlaceToEat placeToEat = db.PlacesToEatSet.Find(id);
            if (placeToEat == null)
            {
                return NotFound();
            }

            db.Entry(placeToEat).Collection(p => p.GuidedTour).Load();

            db.PlacesToEatSet.Remove(placeToEat);
            db.SaveChanges();

            return Ok(placeToEat);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PlaceToEatExists(int id)
        {
            return db.PlacesToEatSet.Count(e => e.Id_PlaceToEat == id) > 0;
        }
    }
}