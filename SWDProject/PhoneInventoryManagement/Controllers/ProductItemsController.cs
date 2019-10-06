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
        public IHttpActionResult GetProductItem()
        {
            var result = db.ProductItem.Select(x => new
            {
                x.ProductItemId,
                x.IMEI,
                x.IsActive,
                x.ProductId,
                x.BillId
            });
            return Ok(result);
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
            var x = new ProductItem
            {
                ProductItemId = productItem.ProductItemId,
                IMEI = productItem.IMEI,
                IsActive = productItem.IsActive,
                ProductId = productItem.ProductId,
                BillId = productItem.BillId
            };
            return Ok(x);
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
                return BadRequest("Parameter id and Bill.BillId error.");
            }
            var x = new ProductItem
            {
                ProductItemId = productItem.ProductItemId,
                IMEI = productItem.IMEI,
                IsActive = productItem.IsActive,
                ProductId = productItem.ProductId,
                BillId = productItem.BillId
            };
            db.Entry(x).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
                return Ok("Update succeed!");
            }
            catch (Exception)
            {
                if (!ProductItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest("The INSERT statement conflicted with the FOREIGN KEY constraint!");
                }
            }
        }

        // POST: api/ProductItems
        [ResponseType(typeof(ProductItem))]
        public IHttpActionResult PostProductItem(ProductItem productItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (ProductItemIMEIExists(productItem.IMEI))
            {
                return BadRequest("IMEI already exist");
            }
            var x = new ProductItem
            {
                IMEI = productItem.IMEI,
                IsActive = productItem.IsActive,
                ProductId = productItem.ProductId,
                BillId = productItem.BillId
            };
            db.ProductItem.Add(x);
            try
            {
                db.SaveChanges();
                return Ok("Insert succeed!");
            }
            catch (Exception)
            {
                return BadRequest("The INSERT statement conflicted with the FOREIGN KEY constraint!");
            }
            //return CreatedAtRoute("DefaultApi", new { id = productItem.ProductItemId }, productItem);
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
            productItem.IsActive = false;
            db.Entry(productItem).State = EntityState.Modified;
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

        private bool ProductItemExists(Guid id)
        {
            return db.ProductItem.Count(e => e.ProductItemId == id) > 0;
        }
        private bool ProductItemIMEIExists(int imei)
        {
            return db.ProductItem.Count(e => e.IMEI == imei) > 0;
        }
    }
}