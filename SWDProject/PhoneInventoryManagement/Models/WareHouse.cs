using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PhoneInventoryManagement.Models
{
    [Table("WareHouse")]
    public class WareHouse
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid WareHouseId { get; set; }

        public string WareHouseName { get; set; }

        public bool IsActive { get; set; }

        public IList<Bill> Bills { get; set; }
    }
}