using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneInventoryManagement.Models
{
    public class RequestLogin
    {
        public int UserName { get; set; }
        public int Password { get; set; }
    }
}