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
        public IQueryable<Bill> GetBill()
        {
            return db.Bill;
        }

        // GET: api/Bills/5
        [ResponseType(typeof(Bill))]
        public IHttpActionResult GetBill(Guid id)
        {
            Bill bill = db.Bill.Find(id);
            if (bill == null)
            {
                return NotFound();
            }

            return Ok(bill);
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
                return BadRequest();
            }

            db.Entry(bill).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BillExists(id))
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

        // POST: api/Bills
        [ResponseType(typeof(Bill))]
        public IHttpActionResult PostBill(Bill bill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Bill.Add(bill);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = bill.BillId }, bill);
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

            db.Bill.Remove(bill);
            db.SaveChanges();

            return Ok(bill);
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