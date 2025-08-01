
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkingWomenApp.Database.Models.Users;

namespace WorkingWomenApp.BLL.Repository
{
    public interface IUserRepository
    {
        DbSet<T> Set<T>() where T : class;
    }
}
