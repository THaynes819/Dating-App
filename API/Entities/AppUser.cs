using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class AppUser
    {
        public int Id { get; set; } // Important to keep the int name Id for the framework to understand it's an identifier convention
        public string UserName { get; set; } // UserName is a convention as well
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}