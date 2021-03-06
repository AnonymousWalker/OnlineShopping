﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShopping.Models.DomainModel
{
    [Table("Product",Schema = "dbo")]
    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }
        public ProductCategoryEnum Category { get; set; }
        [Required]
        public double Price { get; set; }
        public double SalePrice { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual ICollection<ProductImage> Images { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
    }
}