using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineShopping.Models.DomainModel
{
    [Table("Cart",Schema ="dbo")]
    public class Cart
    {
        //[Key]
        //[Column(Order = 0)]
        public int UserId { get; set; }

        //[Key]
        //[Column(Order = 1)]
        public int ProductId { get; set; }

        public virtual User User { get; set; }

        public virtual Product Product { get; set; }

        public int Quantity { get; set; }
    }
}