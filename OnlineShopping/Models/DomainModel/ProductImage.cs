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
        [Required]
        public string ImageName { get; set; }
        [Required]
        public byte[] Content { get; set; }
        public ImageType Type { get; set; } = ImageType.Avatar;
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}