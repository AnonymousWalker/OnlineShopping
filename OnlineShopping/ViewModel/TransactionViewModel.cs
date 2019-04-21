using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShopping.ViewModel
{
    public class TransactionViewModel
    {
        public int TransactionId { get; set; }
        public double TotalAmount { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:MM/dd/yyyy}")]
        public DateTime DatePurchased { get; set; }

        public TransactionViewModel()
        {
            TransactionProducts = new List<CartProductViewModel>();
        }

        public IList<CartProductViewModel> TransactionProducts;
    }
}