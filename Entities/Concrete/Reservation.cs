using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Reservation : BaseEntity   
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid EventId { get; set; }
        public Event Event { get; set; }

        public int TicketCount { get; set; }
        public decimal TotalPrice { get; set; }

        public DateTime ReservationDate { get; set; }
    }
}
