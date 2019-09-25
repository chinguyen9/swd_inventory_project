using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PhoneInventoryManagement.Models
{
    [Table("Bill")]
    public class Bill
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid BillId { get; set; }

        public DateTime DateCreate { get; set; }

        public string BillType { get; set; }

        public bool IsActive { get; set; }

        public Guid UserId { get; set; }

        public Guid WareHouseId { get; set; }

        public Guid? OrderId { get; set; }

        //public Guid ProductItemId { get; set; }

        public WareHouse WareHouse { get; set; }

        //[ForeignKey("UserId")]
        public User User { get; set; }

        //[ForeignKey("OrderId")]
        public Order Order { get; set; }
        //[ForeignKey("ProductItemId")]
        public IList<ProductItem> ProductItems { get; set; }
    }
}