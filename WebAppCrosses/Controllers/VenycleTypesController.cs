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
    public class VenycleTypesController : ApiController
    {
        private GoodWillDbContext db = new GoodWillDbContext();

        // GET: api/VenycleTypes
        public IQueryable<VenycleTypes> GetVenycleTypes()
        {
            return db.VenycleTypes;
        }

        // GET: api/VenycleTypes/5
        [ResponseType(typeof(VenycleTypes))]
        public async Task<IHttpActionResult> GetVenycleTypes(int id)
        {
            VenycleTypes venycleTypes = await db.VenycleTypes.FindAsync(id);
            if (venycleTypes == null)
            {
                return NotFound();
            }

            return Ok(venycleTypes);
        }

        // PUT: api/VenycleTypes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutVenycleTypes(int id, VenycleTypesModel venycleTypes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != venycleTypes.VenycleTypeID)
            {
                return BadRequest();
            }

            var newvenycleTypesmodel = new VenycleTypes();
            CopyModeltoEntity(venycleTypes, newvenycleTypesmodel);

            db.Entry(newvenycleTypesmodel).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VenycleTypesExists(id))
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

        // POST: api/VenycleTypes
        [ResponseType(typeof(VenycleTypes))]
        public async Task<IHttpActionResult> PostVenycleTypes(VenycleTypesModel venycleTypes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newvenycleTypesmodel = new VenycleTypes();
            CopyModeltoEntity(venycleTypes, newvenycleTypesmodel);

            db.VenycleTypes.Add(newvenycleTypesmodel);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = venycleTypes.VenycleTypeID }, venycleTypes);
        }

        // DELETE: api/VenycleTypes/5
        [ResponseType(typeof(VenycleTypes))]
        public async Task<IHttpActionResult> DeleteVenycleTypes(int id)
        {
            VenycleTypes venycleTypes = await db.VenycleTypes.FindAsync(id);
            if (venycleTypes == null)
            {
                return NotFound();
            }

            db.VenycleTypes.Remove(venycleTypes);
            await db.SaveChangesAsync();

            return Ok(venycleTypes);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VenycleTypesExists(int id)
        {
            return db.VenycleTypes.Count(e => e.VenycleTypeID == id) > 0;
        }
    }
}