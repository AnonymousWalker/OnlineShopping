using OnlineShopping.ViewModel.ResultModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace OnlineShopping.ViewModel
{
    public class LoginViewModel : RequestResult
    {
        [Required]
        public string Username { get; set; } = "";
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";
        public bool KeepSignedIn { get; set; } = false;
    }
}