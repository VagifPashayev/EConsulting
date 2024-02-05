using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EConsult.Contracts;
using EConsult.Database.Models;
using System.Reflection.Emit;

namespace EConsult.Database.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .ToTable("Users");

            builder
               .HasOne<UserActivation>(u => u.Activation)
               .WithOne(ua => ua.User)
               .HasForeignKey<UserActivation>(ua => ua.UserId);

            builder
                .HasData(
                new User
                {
                    Id = -1,
                    Name = "Yaver",
                    LastName = "Usta",
                    Email = "super_admin@gmail.com",
                    Password = "$2a$11$Ku17sT3/epVd63/Ptx9lNO6zQcfX6McHLRa0Uj7isuhZvnLztkZmO",
                    Role = Role.Values.SuperAdmin,
                    IsConfirmed = true,
                    UpdatedAt = new DateTime(2023, 09, 06, 0, 0, 0, DateTimeKind.Utc),
                    CreatedAt = new DateTime(2023, 09, 06, 0, 0, 0, DateTimeKind.Utc)
                });
        }
    }
}
