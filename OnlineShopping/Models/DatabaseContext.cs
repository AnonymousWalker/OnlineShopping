using OnlineShopping.Models.DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace OnlineShopping.Models
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext(): base("OnlineShopping")
        {
            Database.SetInitializer<DatabaseContext>(new CreateDatabaseIfNotExists<DatabaseContext>());
        }
            public DbSet<Product> Products { get; set; }
            public DbSet<ProductImage> ProductImages { get; set; }
    }
}