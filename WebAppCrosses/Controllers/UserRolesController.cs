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

namespace WebAppCrosses.Controllers
{
    [Authorize]
    public class UserRolesController : ApiController
    {
        private GoodWillDbContext db = new GoodWillDbContext();

        // GET: api/UserRoles
        public IQueryable<UserRoles> GetUserRoles()
        {
            return db.UserRoles;
        }

        // GET: api/UserRoles/5
        [ResponseType(typeof(UserRoles))]
        public async Task<IHttpActionResult> GetUserRoles(int id)
        {
            UserRoles userRoles = await db.UserRoles.FindAsync(id);
            if (userRoles == null)
            {
                return NotFound();
            }

            return Ok(userRoles);
        }

        // PUT: api/UserRoles/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUserRoles(int id, UserRoles userRoles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userRoles.UserRoleID)
            {
                return BadRequest();
            }

            db.Entry(userRoles).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserRolesExists(id))
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserRolesExists(int id)
        {
            return db.UserRoles.Count(e => e.UserRoleID == id) > 0;
        }
    }
}