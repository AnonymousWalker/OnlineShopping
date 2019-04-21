using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShopping.ViewModel
{
    public class TransactionViewModel
    {
        public int TransactionId { get; set; }
        public double TotalAmount { get; set; }
        public DateTime DatePurchased { get; set; }

        public TransactionViewModel()
        {
            TransactionProducts = new List<CartProductViewModel>();
        }

        public IList<CartProductViewModel> TransactionProducts;
    }
}