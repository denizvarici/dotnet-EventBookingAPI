using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public string PasswordHash { get; set; } 

        public Guid RoleId { get; set; }
        public Role Role { get; set; }

        //nav prop
        public ICollection<Reservation> Reservations { get; set; }
    }
}
