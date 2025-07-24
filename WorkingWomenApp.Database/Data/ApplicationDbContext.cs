using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WorkingWomenApp.Database.Models.Users;

namespace WorkingWomenApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, SecurityRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public ApplicationUser ApplicationUsers { get; set; }
        public SecurityRole SecurityRoles { get; set; }
    }
}
