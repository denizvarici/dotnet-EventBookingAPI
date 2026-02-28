using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Seed
{
    public class EventSeed : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasData(
                new Event()
                {
                    Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                    Title = "Mezuniyet Etkinligi",
                    Description = "Bilgisayar Muhendisleri Mezuniyet Etkinligi",
                    City = "Tekirdag",
                    Venue = "Corlu Muhendislik Fakultesi",
                    EventDate = new DateTime(2026, 12, 12, 0, 0, 0, DateTimeKind.Utc),
                    Capacity = 2000,
                    AvailableSeats = 2000,
                    Price = 100,
                    CreatedAt = new DateTime(2026, 2, 28, 0, 0, 0, DateTimeKind.Utc),
                }
            );
        }
    }
}
