using OnlineShopping.ViewModel.ResultModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace OnlineShopping.ViewModel
{
    public class SignUpViewModel : RequestResult
    {
        public int UserId { get; set; } 
        [Required]
        public string Username { get; set; }
        [MinLength(6), MaxLength(20)]
        public string Password { get; set; }
        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}