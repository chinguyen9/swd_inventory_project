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
    public class ProductItemsController : ApiController
    {
        private PhoneIMDbContext db = new PhoneIMDbContext();

        // GET: api/ProductItems
        public IQueryable<ProductItem> GetProductItem()
        {
            return db.ProductItem;
        }

        // GET: api/ProductItems/5
        [ResponseType(typeof(ProductItem))]
        public IHttpActionResult GetProductItem(Guid id)
        {
            ProductItem productItem = db.ProductItem.Find(id);
            if (productItem == null)
            {
                return NotFound();
            }

            return Ok(productItem);
        }

        // PUT: api/ProductItems/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProductItem(Guid id, ProductItem productItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productItem.ProductItemId)
            {
                return BadRequest();
            }

            db.Entry(productItem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductItemExists(id))
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

        // POST: api/ProductItems
        [ResponseType(typeof(ProductItem))]
        public IHttpActionResult PostProductItem(ProductItem productItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductItem.Add(productItem);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = productItem.ProductItemId }, productItem);
        }

        // DELETE: api/ProductItems/5
        [ResponseType(typeof(ProductItem))]
        public IHttpActionResult DeleteProductItem(Guid id)
        {
            ProductItem productItem = db.ProductItem.Find(id);
            if (productItem == null)
            {
                return NotFound();
            }

            db.ProductItem.Remove(productItem);
            db.SaveChanges();

            return Ok(productItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductItemExists(Guid id)
        {
            return db.ProductItem.Count(e => e.ProductItemId == id) > 0;
        }
    }
}