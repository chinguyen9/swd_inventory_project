using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PhoneInventoryManagement.Models
{
    [Table("ProductItem")]
    public class ProductItem
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ProductItemId { get; set; }

        public int IMEI { get; set; }

        public bool IsActive { get; set; }

        public Guid ProductId { get; set; }

        public Guid BillId { get; set; }

        public Bill Bill { get; set; }

        //[ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}