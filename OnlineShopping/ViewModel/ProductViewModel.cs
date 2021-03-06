﻿using OnlineShopping.Models;
using OnlineShopping.Models.DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopping.ViewModel
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public ProductCategoryEnum Category { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,0.00}", ApplyFormatInEditMode = true)]
        public double Price { get; set; }

        public int Quantity { get; set; } = 1;

        [DisplayFormat(DataFormatString = "{0:#,0.00}", ApplyFormatInEditMode = true)]
        public double SalePrice { get; set; }
        public string Description { get; set; }
        public Nullable<DateTime> DateCreated { get; set; }
        public string ImageSource { get; set; }
        public ProductImage Image { get; set; }
    }
}