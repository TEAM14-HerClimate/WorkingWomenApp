using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

using WorkingWomenApp.BLL.Repository;
using WorkingWomenApp.Data;
using WorkingWomenApp.Database.Core;
using WorkingWomenApp.Database.Models.Climate;
using WorkingWomenApp.Database.Models.Users;

namespace WorkingWomenApp.BLL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        //private readonly IRepository<ApplicationUser> _appUserRepository;
        private readonly IRepository<Article> _articleRepository;
        private readonly IRepository<UserProfile> _profileRepository;
        private readonly IUserRepository _userRepository;


        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        //public IRepository<ApplicationUser> AppUserRepository => _appUserRepository ?? new Repository<ApplicationUser>(_context);
      
        public IUserRepository UserRepository => _userRepository ?? new UserRepository(_context);
        public IRepository<Article> ArticleRepository => _articleRepository ?? new Repository<Article>(_context);
        public IRepository<UserProfile> ProfileRepository => _profileRepository ?? new Repository<UserProfile>(_context);

        public void Dispose()
        {
            if (_context != null) _context.Dispose();
        }
        public void SaveChanges() => _context.SaveChanges();

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

    }
}
