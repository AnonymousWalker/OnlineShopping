using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShopping.Models.DomainModel
{
    [Table("Product",Schema = "dbo")]
    public class Product
    {
        public int ProductId { get; set; }
        [StringLength(50)][Required]
        public string ProductName { get; set; }
        public double Price { get; set; }
        [StringLength(150)]
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual ICollection<ProductImage> Images { get; set; }
    }
}