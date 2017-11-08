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
    public class MotorsController : ApiController
    {
        private GoodWillDbContext db = new GoodWillDbContext();

        // GET: api/Motors
        public IQueryable<Motors> GetMotors()
        {
            return db.Motors;
        }

        // GET: api/Motors/5
        [ResponseType(typeof(Motors))]
        public async Task<IHttpActionResult> GetMotors(int id)
        {
            Motors motors = await db.Motors.FindAsync(id);
            if (motors == null)
            {
                return NotFound();
            }

            return Ok(motors);
        }

        // PUT: api/Motors/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMotors(int id, MotorsModel motors)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != motors.MotorID)
            {
                return BadRequest();
            }

            var newmotorsmodel = new Motors();
            CopyModeltoEntity(motors, newmotorsmodel);

            db.Entry(newmotorsmodel).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MotorsExists(id))
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

        // POST: api/Motors
        [ResponseType(typeof(Motors))]
        public async Task<IHttpActionResult> PostMotors(MotorsModel motors)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newmotorsmodel = new Motors();
            CopyModeltoEntity(motors, newmotorsmodel);

            db.Motors.Add(newmotorsmodel);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = motors.MotorID }, motors);
        }

        // DELETE: api/Motors/5
        [ResponseType(typeof(Motors))]
        public async Task<IHttpActionResult> DeleteMotors(int id)
        {
            Motors motors = await db.Motors.FindAsync(id);
            if (motors == null)
            {
                return NotFound();
            }

            db.Motors.Remove(motors);
            await db.SaveChangesAsync();

            return Ok(motors);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MotorsExists(int id)
        {
            return db.Motors.Count(e => e.MotorID == id) > 0;
        }
    }
}