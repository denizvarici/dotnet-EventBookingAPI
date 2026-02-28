using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.Seed
{
    public class UserSeed : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User()
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Email = "admin@admin.com",
                    FirstName = "AdminFN",
                    LastName = "AdminLN",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123."),
                    RoleId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    CreatedAt = new DateTime(2026, 2, 28, 0, 0, 0, DateTimeKind.Utc)
                },
                new User()
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    Email = "user@user.com",
                    FirstName = "UserFirstName",
                    LastName = "UserLastName",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("User123."),
                    RoleId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    CreatedAt = new DateTime(2026, 2, 28, 0, 0, 0, DateTimeKind.Utc)
                }
            );
        }
    }
}
