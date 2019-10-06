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
    public class BillsController : ApiController
    {
        private PhoneIMDbContext db = new PhoneIMDbContext();

        // GET: api/Bills
        public IHttpActionResult GetBills()
        {
            var result = db.Bill.Select(x => new
            {
                x.BillId,
                x.DateCreate,
                x.BillType,
                x.IsActive,
                x.UserId,
                x.WareHouseId,
                x.OrderId
            });
            return Ok(result);
        }

        // GET: api/Bills/5
        [ResponseType(typeof(Bill))]
        public IHttpActionResult GetBill(Guid id)
        {
            Bill bill = db.Bill.Find(id);
            if (bill == null) return NotFound();
            var x = new Bill()
            {
                BillId = bill.BillId,
                DateCreate = bill.DateCreate,
                BillType = bill.BillType,
                IsActive = bill.IsActive,
                UserId = bill.UserId,
                WareHouseId = bill.WareHouseId,
                OrderId = bill.OrderId
            };
            return Ok(x);
        }

        // PUT: api/Bills/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBill(Guid id, Bill bill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bill.BillId)
            {
                return BadRequest("Parameter id and Bill.BillId error.");
            }
            var x = new Bill()
            {
                BillId = bill.BillId,
                DateCreate = bill.DateCreate,
                BillType = bill.BillType,
                IsActive = bill.IsActive,
                UserId = bill.UserId,
                WareHouseId = bill.WareHouseId,
                OrderId = bill.OrderId
            };

            db.Entry(x).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
                return Ok("Update succeed!");
            }
            catch (Exception)
            {
                if (!BillExists(id))
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

        // POST: api/Bills
        [ResponseType(typeof(Bill))]
        public IHttpActionResult PostBill(Bill bill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Bill x = null;
            if (bill.BillType.Equals("IMPORT"))
            {
                x = new Bill()
                {
                    DateCreate = bill.DateCreate,
                    BillType = bill.BillType,
                    IsActive = bill.IsActive,
                    UserId = bill.UserId,
                    WareHouseId = bill.WareHouseId
                };
            }
            else if (bill.BillType.Equals("EXPORT"))
            {
                x = new Bill()
                {
                    DateCreate = bill.DateCreate,
                    BillType = bill.BillType,
                    IsActive = bill.IsActive,
                    UserId = bill.UserId,
                    WareHouseId = bill.WareHouseId,
                    OrderId = bill.OrderId
                };
            }
            
            db.Bill.Add(x);
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

        // DELETE: api/Bills/5
        [ResponseType(typeof(Bill))]
        public IHttpActionResult DeleteBill(Guid id)
        {
            Bill bill = db.Bill.Find(id);
            if (bill == null)
            {
                return NotFound();
            }
            bill.IsActive = false;
            db.Entry(bill).State = EntityState.Modified;
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

        private bool BillExists(Guid id)
        {
            return db.Bill.Count(e => e.BillId == id) > 0;
        }
    }
}