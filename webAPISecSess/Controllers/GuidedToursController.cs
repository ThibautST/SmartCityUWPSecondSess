using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using webAPISecSess.Models;
using webAPISecSess.ViewModels;

namespace webAPISecSess.Controllers
{
    [Authorize]
    public class GuidedToursController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/GuidedTours
        public IEnumerable<GuidedTour> GetGuidedTourSet()
        {

           var result = db.GuidedTourSet
                                      .Include(b => b.Category)
                                      .Include(a=>a.PlaceToEat)
                                      .Include(c=>c.Transport)
                                      .ToList();
            return result;
           
        }

        // GET: api/GuidedTours/5
        [ResponseType(typeof(GuidedTour))]
        public IHttpActionResult GetGuidedTour(int id)
        {
            GuidedTour guidedTour = db.GuidedTourSet.Find(id);

            if (guidedTour == null)
            {
                return NotFound();
            }

            var rep = db.GuidedTourSet
                                  .Where(b => b.Id_GuidedTour == id)
                                  .Include(c => c.PlaceWithOrder.Select(p => p.TouristPlace)).ToList();

            return Ok(rep);
        }

        [Route("api/GuidedToursWithPlaces/{id:int}")]
        [HttpGet]
        [ResponseType(typeof(GuidedTourViewModel))]
        public IHttpActionResult GetGuidedToursWithPlaces(int id)
        {
            GuidedTour guidedTour = db.GuidedTourSet.Find(id);

            if (guidedTour == null)
            {
                return NotFound();
            }

            db.Entry(guidedTour).Collection(p => p.PlaceWithOrder).Query().OrderBy(x => x.OrderNumber).Include(c => c.TouristPlace).Load();

            List<TouristPlaceViewModel> listPlacesVM = new List<TouristPlaceViewModel>();

            foreach (var t in guidedTour.PlaceWithOrder)
            {
                TouristPlaceViewModel placeVM = new TouristPlaceViewModel
                {
                    Id_TouristPlace = t.TouristPlace.Id_TouristPlace,
                    Latitude = t.TouristPlace.Latitude,
                    Longitude = t.TouristPlace.Longitude,
                    TouristPlaceName = t.TouristPlace.TouristPlaceName,
                    Address = t.TouristPlace.Address,
                    Description = t.TouristPlace.Description,
                    Id_Photo = t.TouristPlace.Id_Photo,
                    Time = t.TouristPlace.Time,
                    Price = t.TouristPlace.Price,
                    RowVersion =t.TouristPlace.RowVersion

                };
                listPlacesVM.Add(placeVM);
            }

            GuidedTourViewModel model =  new GuidedTourViewModel
            {
                Id_GuidedTour = guidedTour.Id_GuidedTour,
                GuidedTourName = guidedTour.GuidedTourName,
                Distance = guidedTour.Distance,
                RowVersion = guidedTour.RowVersion,
                TouristPlaces = listPlacesVM
            };

            return Ok(model);
        }

        // PUT: api/GuidedTours/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGuidedTour(int id, GuidedTour guidedTour)
        {
            if (!GuidedTourExists(id))
            {
                ModelState.AddModelError(string.Empty,
     "Unable to save changes. The entity doesn't exist.");
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != guidedTour.Id_GuidedTour)
            {
                return BadRequest();
            }


            using (var dbcxtransaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.Entry(guidedTour).State = EntityState.Modified;
                    db.SaveChanges();
                    dbcxtransaction.Commit();
                }

                catch (DbUpdateException ex)
                {
                    dbcxtransaction.Rollback();

                    var entry = ex.Entries.Single();
                    var clientValues = (GuidedTour)entry.Entity;
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
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/GuidedTours
        [ResponseType(typeof(GuidedTour))]
        public IHttpActionResult PostGuidedTour(GuidedTour guidedTour)
        {
           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (var dbcxtransaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.GuidedTourSet.Add(guidedTour);
                    db.SaveChanges();
                    dbcxtransaction.Commit();
                }
                catch(Exception ex)
                {
                    dbcxtransaction.Rollback();
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = guidedTour.Id_GuidedTour }, guidedTour);
        }

        // DELETE: api/GuidedTours/5
        [ResponseType(typeof(GuidedTour))]
        public IHttpActionResult DeleteGuidedTour(int id)
        {
            GuidedTour guidedTour = db.GuidedTourSet.Find(id);
            if (guidedTour == null)
            {
                return NotFound();
            }

            using (var dbcxtransaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.GuidedTourSet.Remove(guidedTour);
                    db.SaveChanges();
                    dbcxtransaction.Commit();
                }
                catch (Exception ex)
                {
                    dbcxtransaction.Rollback();
                }
            }


                return Ok(guidedTour);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GuidedTourExists(int id)
        {
            return db.GuidedTourSet.Count(e => e.Id_GuidedTour == id) > 0;
        }
    }
}