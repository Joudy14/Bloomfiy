using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Bloomfiy.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        // store hashed password
        [Required]
        [StringLength(200)]
        public string Password { get; set; }

        [Required]
        [StringLength(50)]
        public string Role { get; set; } // "Admin" or "User"

        // optional fields you may want later
        [StringLength(100)]
        public string FullName { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }
    }
}
