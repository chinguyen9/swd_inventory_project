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
    public class WareHousesController : ApiController
    {
        private PhoneIMDbContext db = new PhoneIMDbContext();

        // GET: api/WareHouses
        public IHttpActionResult GetWareHouse()
        {
            var result = db.WareHouse
                            .Select(x => new
                            {
                                x.WareHouseId,
                                x.WareHouseName,
                                x.IsActive
                            }).ToList();
            return Ok(result);
        }

        // GET: api/WareHouses/5
        [ResponseType(typeof(WareHouse))]
        public IHttpActionResult GetWareHouse(Guid id)
        {
            WareHouse wareHouse = db.WareHouse.Find(id);
            if (wareHouse == null)
            {
                return NotFound();
            }

            return Ok(wareHouse);
        }

        // PUT: api/WareHouses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutWareHouse(Guid id, WareHouse wareHouse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != wareHouse.WareHouseId)
            {
                return BadRequest();
            }

            db.Entry(wareHouse).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WareHouseExists(id))
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

        // POST: api/WareHouses
        [ResponseType(typeof(WareHouse))]
        public IHttpActionResult PostWareHouse(WareHouse wareHouse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.WareHouse.Add(wareHouse);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = wareHouse.WareHouseId }, wareHouse);
        }

        // DELETE: api/WareHouses/5
        [ResponseType(typeof(WareHouse))]
        public IHttpActionResult DeleteWareHouse(Guid id)
        {
            WareHouse wareHouse = db.WareHouse.Find(id);
            if (wareHouse == null)
            {
                return NotFound();
            }

            db.WareHouse.Remove(wareHouse);
            db.SaveChanges();

            return Ok(wareHouse);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WareHouseExists(Guid id)
        {
            return db.WareHouse.Count(e => e.WareHouseId == id) > 0;
        }
    }
}