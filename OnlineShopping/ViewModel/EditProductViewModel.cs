using OnlineShopping.Models.DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopping.ViewModel
{
    public class EditProductViewModel
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Name cannot be empty")]
        [DisplayName("Product Label")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Price cannot be empty")]
        [DisplayName("Price")][Range(0,double.MaxValue)]
        [DisplayFormat(DataFormatString = "{0:#,0.00}", ApplyFormatInEditMode = true)]
        public double Price { get; set; }

        [DisplayName("Sale")][Range(0,double.MaxValue)]
        [DisplayFormat(DataFormatString = "{0:#,0.00}", ApplyFormatInEditMode = true)]
        public double SalePrice { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }
    }
}