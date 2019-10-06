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
    public class ProductsController : ApiController
    {
        private PhoneIMDbContext db = new PhoneIMDbContext();

        // GET: api/Products
        public IHttpActionResult GetProduct()
        {
            var result = db.Product.Select(x => new
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                ProductType = x.ProductType,
                Manufacturer = x.Manufacturer,
                Color = x.Color,
                Description = x.Description,
                ImportPrice = x.ImportPrice,
                IsActive = x.IsActive
            });
            return Ok(result);
        }

        // GET: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProduct(Guid id)
        {
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            var x = new Product()
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductType = product.ProductType,
                Manufacturer = product.Manufacturer,
                Color = product.Color,
                Description = product.Description,
                ImportPrice = product.ImportPrice,
                IsActive = product.IsActive
            };
            return Ok(x);
        }

        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduct(Guid id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.ProductId)
            {
                return BadRequest("Parameter id and Product.ProductId error.");
            }
            var x = new Product()
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductType = product.ProductType,
                Manufacturer = product.Manufacturer,
                Color = product.Color,
                Description = product.Description,
                ImportPrice = product.ImportPrice,
                IsActive = product.IsActive
            };
            db.Entry(x).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
                return Ok("Update succeed!");
            }
            catch (Exception)
            {
                if (!ProductExists(id))
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

        // POST: api/Products
        [ResponseType(typeof(Product))]
        public IHttpActionResult PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var x = new Product()
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductType = product.ProductType,
                Manufacturer = product.Manufacturer,
                Color = product.Color,
                Description = product.Description,
                ImportPrice = product.ImportPrice,
                IsActive = product.IsActive
            };
            db.Product.Add(x);
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

        // DELETE: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult DeleteProduct(Guid id)
        {
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            product.IsActive = false;
            db.Entry(product).State = EntityState.Modified;
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

        private bool ProductExists(Guid id)
        {
            return db.Product.Count(e => e.ProductId == id) > 0;
        }
    }
}