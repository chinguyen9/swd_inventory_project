using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PhoneInventoryManagement.Models
{
    [Table("Product")]
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ProductId { get; set; }

        public string ProductName { get; set; }

        public string ProductType { get; set; }

        public string Manufacturer { get; set; }

        public string Color { get; set; }

        public string Description { get; set; }

        public int ImportPrice { get; set; }

        public bool IsActive { get; set; }

        //public Guid OrderDetailId { get; set; }

        public IList<ProductItem> ProductItems { get; set; }

        //[ForeignKey("OrderDetailId")]
        public IList<OrderDetail> OrderDetails { get; set; }

    }
}