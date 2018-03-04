﻿using OnlineShopping.Models.DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopping.ViewModel
{
    public class EditProductViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public Nullable<DateTime> DateCreated { get; set; }
        public string ImageSource { get; set; }
        public ProductImage Image { get; set; }
    }
}