using PhoneInventoryManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace PhoneInventoryManagement.Models
{
    [Table("User")]
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public int PhoneNumber { get; set; }

        public bool IsActive { get; set; }

        public Guid RoleId { get; set; }

        //[ForeignKey("RoleId")]
        public Role Role { get; set; }

        public IList<Bill> Bills { get; set; }

        public IList<Order> Orders { get; set; }
    }
}