using OnlineShopping.Models.DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopping.ViewModel
{
    public class EditProductViewModel
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Name cannot be empty")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Price cannot be empty")]
        public double Price { get; set; }
        public string Description { get; set; }
    }
}