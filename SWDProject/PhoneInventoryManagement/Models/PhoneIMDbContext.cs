using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PhoneInventoryManagement.Models
{
    public class PhoneIMDbContext : DbContext
    {
        public PhoneIMDbContext() : base("Name=PhoneIMDb")
        {

        }
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Bill> Bill { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductItem> ProductItem { get; set; }
        public DbSet<WareHouse> WareHouse { get; set; }
    }
}