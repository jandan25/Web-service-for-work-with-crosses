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
using System.Web.Http.Tracing;

namespace WebAppCrosses.Controllers
{
    [Authorize]
    public class CarModelsController : ApiController
    {
        private GoodWillDbContext db = new GoodWillDbContext();

        // GET: api/CarModels
        public IQueryable<CarModels> GetCarModels()
        {
            return db.CarModels;
        }

        // GET: api/CarModels/5
        [ResponseType(typeof(CarModels))]
        public async Task<IHttpActionResult> GetCarModels(int id)
        {
            CarModels carModels = await db.CarModels.FindAsync(id);
            if (carModels == null)
            {
                return NotFound();
            }

            return Ok(carModels);
        }

        // PUT: api/CarModels/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCarModels(int id, CarModelsModel carModels)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != carModels.CarModelID)
            {
                return BadRequest();
            }

            var newcarmodel = new CarModels();
            CopyModeltoEntity(carModels, newcarmodel);

            db.Entry(newcarmodel).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarModelsExists(id))
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

        // POST: api/CarModels
        [ResponseType(typeof(CarModels))]
        public async Task<IHttpActionResult> PostCarModels(CarModelsModel carModels)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newcarmodel = new CarModels();
            CopyModeltoEntity(carModels, newcarmodel);

            db.CarModels.Add(newcarmodel);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = carModels.CarModelID }, carModels);
        }

        // DELETE: api/CarModels/5
        [ResponseType(typeof(CarModels))]
        public async Task<IHttpActionResult> DeleteCarModels(int id)
        {
            CarModels carModels = await db.CarModels.FindAsync(id);
            if (carModels == null)
            {
                return NotFound();
            }

            db.CarModels.Remove(carModels);
            await db.SaveChangesAsync();

            return Ok(carModels);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CarModelsExists(int id)
        {
            return db.CarModels.Count(e => e.CarModelID == id) > 0;
        }
    }
}