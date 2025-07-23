using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using WorkingWomenApp.BLL.Repository;
using WorkingWomenApp.BLL.UnitOfWork;
using WorkingWomenApp.Data;
using WorkingWomenApp.Database.Models.Users;


namespace WorkingWomenApp.Persistent;

public static class IServiceExtensions
{
    public static IServiceCollection BuildingPersistentServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Add services to the container
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("ApplicationDefaultConnection"),
                o => o.UseCompatibilityLevel(120));
        });



        services.AddMemoryCache();
      services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<DbContext, ApplicationDbContext>();

        services.AddTransient(typeof(IRepository<>), typeof(Repository<>));


        //services.AddSingleton<DataIntialiser>();

        services.AddIdentity<ApplicationUser, SecurityRole>(
                options => options.SignIn.RequireConfirmedAccount = true
            )
            .AddEntityFrameworkStores<ApplicationDbContext>().AddRoles<SecurityRole>()
            .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);

        services.Configure<DataProtectionTokenProviderOptions>(options =>
        {
            options.TokenLifespan = TimeSpan.FromHours(3); // Set token expiration to 3 hours
        });
        return services;
    }
}
