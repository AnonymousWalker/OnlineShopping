using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShopping.Models.DomainModel
{
    [Table("Transaction", Schema = "dbo")]
    public class Transaction
    {
        public int TransactionId { get; set; }
        [Required]
        public double TotalAmount { get; set; }
        [Required]
        public DateTime Date { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}