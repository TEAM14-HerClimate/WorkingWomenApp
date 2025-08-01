using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WorkingWomenApp.Database.Models.Climate;
using WorkingWomenApp.Database.Models.Users;

namespace WorkingWomenApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, SecurityRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<SecurityRole> SecurityRoles { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<UserProfile> UserProfile { get; set; }
        
    }
}
