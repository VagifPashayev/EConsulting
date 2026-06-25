using EConsult.Contracts;
using EConsult.Database.Models;

namespace EConsult.Database;

public static class DemoDataSeeder
{
    public static void Seed(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<EConsultDbContext>();

        if (dbContext.Products.Any())
            return;

        dbContext.Products.AddRange(
            new Product(
                "Technology Strategy",
                "Architecture and technology planning for growing companies.",
                90,
                null,
                "/client/assets/images/blog-layouts-1-270x184.jpg"),
            new Product(
                "Business Analysis",
                "Operational analysis and process improvement consulting.",
                75,
                null,
                "/client/assets/images/blog-layouts-2-270x184.jpg"),
            new Product(
                "Financial Planning",
                "Practical financial planning for small and medium businesses.",
                85,
                null,
                "/client/assets/images/blog-layouts-3-270x184.jpg"),
            new Product(
                "Growth Consulting",
                "Market research and a focused roadmap for sustainable growth.",
                95,
                null,
                "/client/assets/images/blog-layouts-4-270x184.jpg"));

        dbContext.Users.Add(new User
        {
            Name = "Demo",
            LastName = "Administrator",
            Email = "admin@econsult.local",
            Password = BCrypt.Net.BCrypt.HashPassword("Demo123!"),
            IsConfirmed = true,
            Role = Role.Values.SuperAdmin
        });

        dbContext.SaveChanges();
    }
}
