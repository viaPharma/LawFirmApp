using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawFirmApp.Models
{

    public class User
    {
        public int Id { get; set; } // Unique identifier for the user
        public required string Username { get; set; }
        public required string Password { get; set; }
        public int RoleId { get; set; } // Foreign key to RoleType
        public required RoleType Role { get; set; } // Navigation property for RoleType
    }

}
