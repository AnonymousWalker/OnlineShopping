using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShopping.Models.DomainModel
{
    [Table("ProductImage",Schema = "dbo")]
    public class ProductImage
    {
        [Key]
        public int ImageId { get; set; }
        public string ImageName { get; set; }
        public byte[] Content { get; set; }
        public ImageType Type { get; set; }
        public int ProductId { get; set; }
        [StringLength(50)]
        public virtual Product Product { get; set; }
    }
}