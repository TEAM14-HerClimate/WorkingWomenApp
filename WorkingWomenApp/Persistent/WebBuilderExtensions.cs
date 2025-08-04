using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using WorkingWomenApp.Data;
using WorkingWomenApp.Database.Models.Users;
using WorkingWomenApp.Database.SeedData;

namespace WorkingWomenApp.Persistent;

public static class WebBuilderExtensions
{
    public static IServiceScope RunMigration(this WebApplication app)
    {
        var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();

        return scope;
    }

    public static void SeedData(this IServiceScope scope)
    {
        try
        {
            var services = scope.ServiceProvider;

            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<SecurityRole>>();

            services.GetRequiredService<DataIntialiser>();
            DataIntialiser.SeedData(userManager, roleManager);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while seeding the database: {ex.Message}");
            throw;
        }
    }


    public static WebApplication BuildStaticFiles(this WebApplication app, WebApplicationBuilder builder)
    {
        var fileStoragePath = builder.Configuration["FileStoragePath"]
            ?? Path.Combine(builder.Environment.ContentRootPath, "FileStorage");

        if (!Directory.Exists(fileStoragePath))
        {
            Directory.CreateDirectory(fileStoragePath);
        }
        var staticFileOptions = new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(fileStoragePath),
            RequestPath = "/FileStorage"
        };
        app.UseStaticFiles(staticFileOptions);

        return app;
    }
}
