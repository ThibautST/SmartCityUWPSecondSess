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
    public class TransportsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Transports
        public IEnumerable<Transport> GetTransportSet()
        {
            return db.TransportSet.ToList();
        }

        // GET: api/Transports/5
        [ResponseType(typeof(Transport))]
        public IHttpActionResult GetTransport(int id)
        {
            Transport transport = db.TransportSet.Find(id);
            if (transport == null)
            {
                return NotFound();
            }

            return Ok(transport);
        }

        // PUT: api/Transports/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTransport(int id, Transport transport)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != transport.Id_Transport)
            {
                return BadRequest();
            }

            db.Entry(transport).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransportExists(id))
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

        // POST: api/Transports
        [ResponseType(typeof(Transport))]
        public IHttpActionResult PostTransport(Transport transport)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TransportSet.Add(transport);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = transport.Id_Transport }, transport);
        }

        // DELETE: api/Transports/5
        
        [ResponseType(typeof(Transport))]
        public IHttpActionResult DeleteTransport(int id)
        {
            Transport transport = db.TransportSet.Find(id);
            if (transport == null)
            {
                return NotFound();
            }

            db.TransportSet.Remove(transport);
            db.SaveChanges();

            return Ok(transport);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TransportExists(int id)
        {
            return db.TransportSet.Count(e => e.Id_Transport == id) > 0;
        }
    }
}