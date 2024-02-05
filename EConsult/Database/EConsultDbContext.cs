using Microsoft.EntityFrameworkCore;
using EConsult.Contracts;
using EConsult.Database.Base;
using EConsult.Database.Configurations;
using EConsult.Database.Models;

namespace EConsult.Database;

public class EConsultDbContext : DbContext
{
    public EConsultDbContext(DbContextOptions<EConsultDbContext> options)
        : base(options) { }

    public override int SaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is not IAuditable)
                continue;

            IAuditable auditable = (IAuditable)entry.Entity;

            if (entry.State == EntityState.Added)
            {
                auditable.CreatedAt = DateTime.UtcNow;
                auditable.UpdatedAt = DateTime.UtcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                auditable.UpdatedAt = DateTime.UtcNow;
            }
        }


        return base.SaveChanges();
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EConsultDbContext).Assembly);

        #region Categories

        modelBuilder
            .Entity<Category>()
            .HasData(
                new Category
                {
                    Id = -1,
                    Name = "IT",
                    UpdatedAt = new DateTime(2023, 08, 30, 0, 0, 0, DateTimeKind.Utc),
                    CreatedAt = new DateTime(2023, 08, 30, 0, 0, 0, DateTimeKind.Utc),
                },
                new Category
                {
                    Id = -2,
                    Name = "Business",
                    UpdatedAt = new DateTime(2023, 08, 30, 0, 0, 0, DateTimeKind.Utc),
                    CreatedAt = new DateTime(2023, 08, 30, 0, 0, 0, DateTimeKind.Utc),
                },
                new Category
                {
                    Id = -3,
                    Name = "Agro",
                    UpdatedAt = new DateTime(2023, 08, 30, 0, 0, 0, DateTimeKind.Utc),
                    CreatedAt = new DateTime(2023, 08, 30, 0, 0, 0, DateTimeKind.Utc),
                },
                new Category
                {
                    Id = -4,
                    Name = "Sport",
                    UpdatedAt = new DateTime(2023, 08, 30, 0, 0, 0, DateTimeKind.Utc),
                    CreatedAt = new DateTime(2023, 08, 30, 0, 0, 0, DateTimeKind.Utc),
                });


        #endregion


        base.OnModelCreating(modelBuilder);
    }


    public DbSet<Product> Products { get; set; }
    public DbSet<CategoryProduct> CategoryProducts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<EmailMessage> EmailMessages { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<UserActivation> UserActivations { get; set; }
    public DbSet<AlertMessage> AlertMessages { get; set; }
}
