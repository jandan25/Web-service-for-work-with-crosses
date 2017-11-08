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
using CrossEntities;
using WebAppCrosses.Models;
using static WebAppCrosses.Utils;

namespace WebAppCrosses.Controllers
{
    [Authorize]
    public class ManufactorsController : ApiController
    {
        private GoodWillDbContext db = new GoodWillDbContext();

        // GET: api/Manufactors
        public IQueryable<Manufactors> GetManufactors()
        {
            return db.Manufactors;
        }

        // GET: api/Manufactors/5
        [ResponseType(typeof(Manufactors))]
        public async Task<IHttpActionResult> GetManufactors(int id)
        {
            Manufactors manufactors = await db.Manufactors.FindAsync(id);
            if (manufactors == null)
            {
                return NotFound();
            }

            return Ok(manufactors);
        }

        // PUT: api/Manufactors/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutManufactors(int id, ManufactorsModel manufactors)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != manufactors.ManufactorID)
            {
                return BadRequest();
            }

            var newmanufactorsmodel = new Manufactors();
            CopyModeltoEntity(manufactors, newmanufactorsmodel);

            db.Entry(newmanufactorsmodel).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ManufactorsExists(id))
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

        // POST: api/Manufactors
        [ResponseType(typeof(Manufactors))]
        public async Task<IHttpActionResult> PostManufactors(ManufactorsModel manufactors)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newmanufactorsmodel = new Manufactors();
            CopyModeltoEntity(manufactors, newmanufactorsmodel);

            db.Manufactors.Add(newmanufactorsmodel);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = manufactors.ManufactorID }, manufactors);
        }

        // DELETE: api/Manufactors/5
        [ResponseType(typeof(Manufactors))]
        public async Task<IHttpActionResult> DeleteManufactors(int id)
        {
            Manufactors manufactors = await db.Manufactors.FindAsync(id);
            if (manufactors == null)
            {
                return NotFound();
            }

            db.Manufactors.Remove(manufactors);
            await db.SaveChangesAsync();

            return Ok(manufactors);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ManufactorsExists(int id)
        {
            return db.Manufactors.Count(e => e.ManufactorID == id) > 0;
        }
    }
}