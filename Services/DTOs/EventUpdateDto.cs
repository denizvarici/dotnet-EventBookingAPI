using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTOs
{
    public class EventUpdateDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Venue { get; set; } = null!;
        public DateTime EventDate { get; set; }
        public int Capacity { get; set; }
        public decimal Price { get; set; }
    }
}
