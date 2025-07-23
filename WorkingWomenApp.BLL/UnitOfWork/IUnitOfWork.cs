using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using WorkingWomenApp.BLL.Repository;
using WorkingWomenApp.Data;
using WorkingWomenApp.Database.Core;
using System.Data;

using WorkingWomenApp.Database.Models.Users;

namespace WorkingWomenApp.BLL.UnitOfWork
{
   
        public interface IUnitOfWork : IDisposable
        {
            IRepository<ApplicationUser> AppUserRepository { get; }

           
            IUserRepository UserRepository { get; }
         

            void SaveChanges();
            Task SaveChangesAsync();
        }
    
}
