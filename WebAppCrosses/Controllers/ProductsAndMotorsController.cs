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
    public class ProductsAndMotorsController : ApiController
    {
        private GoodWillDbContext db = new GoodWillDbContext();

        // GET: api/ProductsAndMotors
        public IQueryable<ProductsAndMotors> GetProductsAndMotors()
        {
            return db.ProductsAndMotors;
        }

        // GET: api/ProductsAndMotors/5
        [ResponseType(typeof(ProductsAndMotors))]
        public async Task<IHttpActionResult> GetProductsAndMotors(int id)
        {
            ProductsAndMotors productsAndMotors = await db.ProductsAndMotors.FindAsync(id);
            if (productsAndMotors == null)
            {
                return NotFound();
            }

            return Ok(productsAndMotors);
        }

        // PUT: api/ProductsAndMotors/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProductsAndMotors(int id, ProductsAndMotorsModel productsAndMotors)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productsAndMotors.ProductAndMotorID)
            {
                return BadRequest();
            }

            var newproductsAndMotorsmodel = new ProductsAndMotors();
            CopyModeltoEntity(productsAndMotors, newproductsAndMotorsmodel);

            db.Entry(newproductsAndMotorsmodel).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsAndMotorsExists(id))
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

        // POST: api/ProductsAndMotors
        [ResponseType(typeof(ProductsAndMotors))]
        public async Task<IHttpActionResult> PostProductsAndMotors(ProductsAndMotorsModel productsAndMotors)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newproductsAndMotorsmodel = new ProductsAndMotors();
            CopyModeltoEntity(productsAndMotors, newproductsAndMotorsmodel);

            db.ProductsAndMotors.Add(newproductsAndMotorsmodel);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = productsAndMotors.ProductAndMotorID }, productsAndMotors);
        }

        // DELETE: api/ProductsAndMotors/5
        [ResponseType(typeof(ProductsAndMotors))]
        public async Task<IHttpActionResult> DeleteProductsAndMotors(int id)
        {
            ProductsAndMotors productsAndMotors = await db.ProductsAndMotors.FindAsync(id);
            if (productsAndMotors == null)
            {
                return NotFound();
            }

            db.ProductsAndMotors.Remove(productsAndMotors);
            await db.SaveChangesAsync();

            return Ok(productsAndMotors);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductsAndMotorsExists(int id)
        {
            return db.ProductsAndMotors.Count(e => e.ProductAndMotorID == id) > 0;
        }
    }
}