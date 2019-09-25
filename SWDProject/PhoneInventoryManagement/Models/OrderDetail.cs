using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PhoneInventoryManagement.Models
{
    [Table("OrderDetail")]
    public class OrderDetail
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid OrderDetailId { get; set; }

        public int Quantity { get; set; }

        public int SalePrice { get; set; }

        public bool IsActive { get; set; }

        public Guid ProductId { get; set; }

        public Guid OrderId { get; set; }

        public Order Order { get; set; }

        public Product Product { get; set; }

    }
}