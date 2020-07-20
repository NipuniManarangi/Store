using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShoppingCart.Data.Models
{
    public class User
    {
      
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; }
        [Key]
        [Required]
        [Column(TypeName = "nvarchar(60)")]
        public string Email { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string ContatctNo { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(60)")]
        public string Address_Line1 { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(60)")]
        public string Address_Line2 { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(60)")]
        public string State { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(60)")]
        public string PostalCode { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Password { get; set; }
    }
}
