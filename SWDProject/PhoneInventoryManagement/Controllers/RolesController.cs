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
using PhoneInventoryManagement.Models;

namespace PhoneInventoryManagement.Controllers
{
    public class RolesController : ApiController
    {
        private PhoneIMDbContext db = new PhoneIMDbContext();

        // GET: api/Roles
        public IHttpActionResult GetRole()
        {
            var result = db.Role.Select(x => new
            {
                x.RoleId,
                x.RoleName,
                x.IsActive
            });
            return Ok(result);
        }

        // GET: api/Roles/5
        [ResponseType(typeof(Role))]
        public IHttpActionResult GetRole(Guid id)
        {
            Role role = db.Role.Find(id);
            if (role == null)
            {
                return NotFound();
            }
            var x = new Role
            {
                RoleId = role.RoleId,
                RoleName = role.RoleName,
                IsActive = role.IsActive
            };
            return Ok(x);
        }

        // PUT: api/Roles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRole(Guid id, Role role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != role.RoleId)
            {
                return BadRequest("Parameter id and Role.RoleId error.");
            }
            var x = new Role
            {
                RoleId = role.RoleId,
                RoleName = role.RoleName,
                IsActive = role.IsActive
            };
            db.Entry(x).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
                return Ok("Update succeed!");
            }
            catch (Exception)
            {
                if (!RoleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    //throw ex;
                    return BadRequest("The INSERT statement conflicted with the FOREIGN KEY constraint!");
                }
            }
        }

        // POST: api/Roles
        [ResponseType(typeof(Role))]
        public IHttpActionResult PostRole(Role role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var x = new Role
            {
                RoleName = role.RoleName,
                IsActive = role.IsActive
            };

            db.Role.Add(x);
            try
            {
                db.SaveChanges();
                //return CreatedAtRoute("DefaultApi", new { id = bill.BillId }, bill);
                return Ok("Insert succeed!");
            }
            catch (Exception)
            {
                return BadRequest("The INSERT statement conflicted with the FOREIGN KEY constraint!");
            }
        }

        // DELETE: api/Roles/5
        [ResponseType(typeof(Role))]
        public IHttpActionResult DeleteRole(Guid id)
        {
            Role role = db.Role.Find(id);
            if (role == null)
            {
                return NotFound();
            }
            role.IsActive = false;
            db.Entry(role).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
                return Ok("Delete succeed!");
            }
            catch (Exception)
            {
                return BadRequest("Delete failed!");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RoleExists(Guid id)
        {
            return db.Role.Count(e => e.RoleId == id) > 0;
        }
    }
}