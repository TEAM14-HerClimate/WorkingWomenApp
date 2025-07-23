using System.Linq.Expressions;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorkingWomenApp.Data;
using WorkingWomenApp.Database.Models.Users;

namespace WorkingWomenApp.BLL.Repository
{
    public class UserRepository: Repository<ApplicationUser>, IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }


    }
}
