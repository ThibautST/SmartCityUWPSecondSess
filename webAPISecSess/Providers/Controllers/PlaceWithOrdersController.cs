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
    public class PlaceWithOrdersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/PlaceWithOrders
        public IQueryable<PlaceWithOrder> GetPlaceWithOrderSet()
        {
            return db.PlaceWithOrderSet;
        }

        // GET: api/PlaceWithOrders/5
        [ResponseType(typeof(PlaceWithOrder))]
        public IHttpActionResult GetPlaceWithOrder(int id)
        {
            PlaceWithOrder placeWithOrder = db.PlaceWithOrderSet.Find(id);
            if (placeWithOrder == null)
            {
                return NotFound();
            }

            return Ok(placeWithOrder);
        }

        // PUT: api/PlaceWithOrders/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPlaceWithOrder(int id, PlaceWithOrder placeWithOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != placeWithOrder.Id_GuidedTour)
            {
                return BadRequest();
            }

            db.Entry(placeWithOrder).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlaceWithOrderExists(id))
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

        // POST: api/PlaceWithOrders
        [ResponseType(typeof(PlaceWithOrder))]
        public IHttpActionResult PostPlaceWithOrder(PlaceWithOrder placeWithOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PlaceWithOrderSet.Add(placeWithOrder);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PlaceWithOrderExists(placeWithOrder.Id_GuidedTour))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = placeWithOrder.Id_GuidedTour }, placeWithOrder);
        }

        // DELETE: api/PlaceWithOrders/5
        [ResponseType(typeof(PlaceWithOrder))]
        public IHttpActionResult DeletePlaceWithOrder(int id)
        {
            PlaceWithOrder placeWithOrder = db.PlaceWithOrderSet.Find(id);
            if (placeWithOrder == null)
            {
                return NotFound();
            }

            db.PlaceWithOrderSet.Remove(placeWithOrder);
            db.SaveChanges();

            return Ok(placeWithOrder);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PlaceWithOrderExists(int id)
        {
            return db.PlaceWithOrderSet.Count(e => e.Id_GuidedTour == id) > 0;
        }
    }
}