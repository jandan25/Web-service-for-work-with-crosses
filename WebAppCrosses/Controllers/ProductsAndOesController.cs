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
using System.Web.Mvc;
using System.Web.UI;

namespace WebAppCrosses.Controllers
{
    [Authorize]
    public class ProductsAndOesController : ApiController
    {
        private GoodWillDbContext db = new GoodWillDbContext();

        // GET: api/ProductsAndOes
        public IQueryable<ProductsAndOes> GetProductsAndOes()
        {
            return db.ProductsAndOes;
        }

        // GET: api/Crosses
        [System.Web.Http.Route("api/Crosses")]
        //server 5600ms //client 3906 //wt 5169 //ds 4296
        [OutputCache(Duration = 3600, Location = OutputCacheLocation.Client)]
        public async Task<IHttpActionResult> GetCrosses()
        {
            using (var unitOfWork = new Repositories.UnitOfWork())
            {
                var result = unitOfWork.GetCrossSelection().ToList();

                IEnumerable<CrossSelectionResult> records = (
                    from prGetCrossses in result
                    select new CrossSelectionResult
                    {
                       Model = prGetCrossses.Model,
                       ManufactorTS = prGetCrossses.ManufactorTS,
                       Code = prGetCrossses.Code,
                       MotorName = prGetCrossses.MotorName,
                       Comment = prGetCrossses.Comment,
                       MotorEngine = prGetCrossses.MotorEngine,
                       TypeTs = prGetCrossses.TypeTs
                    }).ToList();

                return Ok(records);
            }
        }

        // GET: api/ProductsAndOes/5
        [ResponseType(typeof(ProductsAndOes))]
        public async Task<IHttpActionResult> GetProductsAndOes(int id)
        {
            ProductsAndOes productsAndOes = await db.ProductsAndOes.FindAsync(id);
            if (productsAndOes == null)
            {
                return NotFound();
            }

            return Ok(productsAndOes);
        }

        // PUT: api/ProductsAndOes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProductsAndOes(int id, ProductsAndOesModel productsAndOes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productsAndOes.ProductAndOeID)
            {
                return BadRequest();
            }

            var newproductsAndOesmodel = new ProductsAndOes();
            CopyModeltoEntity(productsAndOes, newproductsAndOesmodel);

            db.Entry(newproductsAndOesmodel).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsAndOesExists(id))
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

        // POST: api/ProductsAndOes
        [ResponseType(typeof(ProductsAndOes))]
        public async Task<IHttpActionResult> PostProductsAndOes(ProductsAndOesModel productsAndOes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newproductsAndOesmodel = new ProductsAndOes();
            CopyModeltoEntity(productsAndOes, newproductsAndOesmodel);

            db.ProductsAndOes.Add(newproductsAndOesmodel);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = productsAndOes.ProductAndOeID }, productsAndOes);
        }

        // DELETE: api/ProductsAndOes/5
        [ResponseType(typeof(ProductsAndOes))]
        public async Task<IHttpActionResult> DeleteProductsAndOes(int id)
        {
            ProductsAndOes productsAndOes = await db.ProductsAndOes.FindAsync(id);
            if (productsAndOes == null)
            {
                return NotFound();
            }

            db.ProductsAndOes.Remove(productsAndOes);
            await db.SaveChangesAsync();

            return Ok(productsAndOes);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductsAndOesExists(int id)
        {
            return db.ProductsAndOes.Count(e => e.ProductAndOeID == id) > 0;
        }
    }
}