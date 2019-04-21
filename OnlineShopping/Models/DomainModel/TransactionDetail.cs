using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineShopping.Models.DomainModel
{
    [Table("TransactionDetail", Schema = "dbo")]
    public class TransactionDetail
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int TransactionId { get; set; }
        public int Quantity { get; set; }
        public double Amount { get; set; }  //the product price can be changed later

        public virtual Transaction Transaction { get; set; }
        public virtual Product Product { get; set; }
    }
}