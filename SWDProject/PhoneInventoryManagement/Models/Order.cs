using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PhoneInventoryManagement.Models
{
    [Table("Order")]
    public class Order
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid OrderId { get; set; }

        public string CustomerName { get; set; }

        public DateTime DateCreate { get; set; }

        public int TotalPrice { get; set; }

        public bool IsActive { get; set; }

        public Guid UserId { get; set; }

        public IList<Bill> Bills { get; set; }

        //[ForeignKey("UserId")]
        public User User { get; set; }
            
        //[ForeignKey("OrderDetailId")]
        public IList<OrderDetail> OrderDetails { get; set; }

    }
}