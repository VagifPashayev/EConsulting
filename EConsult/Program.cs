using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EConsult.Database;
using EConsult.Services.Abstracts;
using EConsult.Services.Concretes;
using EConsult.Hubs;

namespace EConsult;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //Services
        builder.Services
            .AddControllersWithViews()
            .AddRazorRuntimeCompilation();

        builder.Services
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(o =>
            {
                o.Cookie.Name = "EConsultIdentity";
                o.LoginPath = "/auth/login";
            });

        builder.Services
            .AddDbContext<EConsultDbContext>(o =>
            {
                o.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            })
            .AddScoped<IUserService, UserService>()
            .AddSingleton<IFileService, ServerFileService>()
            .AddScoped<IEmailService, EmailService>()
            .AddScoped<IOrderService, OrderService>()
            .AddScoped<INotificationService, NotificationService>()
            .AddScoped<IUserActivationService, UserActivationService>()
            .AddSingleton<IActionContextAccessor, ActionContextAccessor>()
            .AddHttpContextAccessor()
            .AddHttpClient()
            .AddSingleton<IAlertMessageService, AlertMessageService>()
            .AddSingleton<IChatService, ChatService>()
            .AddSingleton<OnlineUserTracker>();

        builder.Services
            .AddSignalR();

        var app = builder.Build();

        app.UseStaticFiles();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute("default", "{controller=Home}/{action=Index}");

        app.MapHub<AlertMessageHub>("/alert-hub"); 
        app.MapHub<OnlineUserHub>("/online-user-hub"); 
        app.MapHub<StaffUsersViewHub>("/staff-users-view-hub"); 
        app.MapHub<ChatHub>("/conference-hub"); 

        app.Run();
    }
}