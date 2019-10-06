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
    public class OrderDetailsController : ApiController
    {
        private PhoneIMDbContext db = new PhoneIMDbContext();

        // GET: api/OrderDetails
        public IHttpActionResult GetOrderDetail()
        {
            var result = db.OrderDetail.Select(x => new 
            {
                x.OrderDetailId,
                x.Quantity,
                x.SalePrice,
                x.IsActive,
                x.ProductId,
                x.OrderId
            });
            return Ok(result);
        }

        // GET: api/OrderDetails/5
        [ResponseType(typeof(OrderDetail))]
        public IHttpActionResult GetOrderDetail(Guid id)
        {
            OrderDetail orderDetail = db.OrderDetail.Find(id);
            if (orderDetail == null)
            {
                return NotFound();
            }
            var x = new OrderDetail()
            {
                OrderDetailId=orderDetail.OrderDetailId,
                Quantity=orderDetail.Quantity,
                SalePrice=orderDetail.SalePrice,
                IsActive=orderDetail.IsActive,
                ProductId=orderDetail.ProductId,
                OrderId=orderDetail.OrderId
            };
            return Ok(x);
        }

        // PUT: api/OrderDetails/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrderDetail(Guid id, OrderDetail orderDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != orderDetail.OrderDetailId)
            {
                return BadRequest("Parameter id and OrderDetail.OrderDetailId error.");
            }
            var x = new OrderDetail()
            {
                OrderDetailId = orderDetail.OrderDetailId,
                Quantity = orderDetail.Quantity,
                SalePrice = orderDetail.SalePrice,
                IsActive = orderDetail.IsActive,
                ProductId = orderDetail.ProductId,
                OrderId = orderDetail.OrderId
            };
            db.Entry(x).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
                return Ok("Update succeed!");
            }
            catch (Exception)
            {
                if (!OrderDetailExists(id))
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

        // POST: api/OrderDetails
        [ResponseType(typeof(OrderDetail))]
        public IHttpActionResult PostOrderDetail(OrderDetail orderDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var x = new OrderDetail()
            {
                Quantity = orderDetail.Quantity,
                SalePrice = orderDetail.SalePrice,
                IsActive = orderDetail.IsActive,
                ProductId = orderDetail.ProductId,
                OrderId = orderDetail.OrderId
            };
            db.OrderDetail.Add(x);
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

        // DELETE: api/OrderDetails/5
        [ResponseType(typeof(OrderDetail))]
        public IHttpActionResult DeleteOrderDetail(Guid id)
        {
            OrderDetail orderDetail = db.OrderDetail.Find(id);
            if (orderDetail == null)
            {
                return NotFound();
            }
            orderDetail.IsActive = false;
            db.Entry(orderDetail).State = EntityState.Modified;
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

        private bool OrderDetailExists(Guid id)
        {
            return db.OrderDetail.Count(e => e.OrderDetailId == id) > 0;
        }
    }
}