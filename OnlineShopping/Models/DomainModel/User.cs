using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineShopping.Models.DomainModel
{
    [Table("User", Schema = "dbo")]
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string Username { get; set; }

        [MinLength(6)]
        [MaxLength(20)]
        public string Password { get; set; }

        public string Email { get; set; }

        [MaxLength(20)]
        public string FirstName { get; set; }

        [MaxLength(20)]
        public string LastName { get; set; }

        public virtual ICollection<Cart> Carts { get; set; }
    }
}