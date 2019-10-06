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
            var x = new WareHouse()
            {
                WareHouseId = wareHouse.WareHouseId,
                WareHouseName = wareHouse.WareHouseName,
                IsActive = wareHouse.IsActive
            };
            return Ok(x);
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
                return BadRequest("Parameter id and Bill.BillId error.");
            }
            var x = new WareHouse()
            {
                WareHouseId = wareHouse.WareHouseId,
                WareHouseName = wareHouse.WareHouseName,
                IsActive = wareHouse.IsActive
            };
            db.Entry(x).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
                return Ok("Update succeed!");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WareHouseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    //throw;
                    return BadRequest("Update failed!");
                }
            }
        }

        // POST: api/WareHouses
        [ResponseType(typeof(WareHouse))]
        public IHttpActionResult PostWareHouse(WareHouse wareHouse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var x = new WareHouse()
            {
                WareHouseName = wareHouse.WareHouseName,
                IsActive = wareHouse.IsActive
            };
            db.WareHouse.Add(x);
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

        // DELETE: api/WareHouses/5
        [ResponseType(typeof(WareHouse))]
        public IHttpActionResult DeleteWareHouse(Guid id)
        {
            WareHouse wareHouse = db.WareHouse.Find(id);
            if (wareHouse == null)
            {
                return NotFound();
            }
            wareHouse.IsActive = false;
            db.Entry(wareHouse).State = EntityState.Modified;
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

        private bool WareHouseExists(Guid id)
        {
            return db.WareHouse.Count(e => e.WareHouseId == id) > 0;
        }
    }
}