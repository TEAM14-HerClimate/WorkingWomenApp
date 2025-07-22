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

namespace WorkingWomenApp.BLL.UnitOfWork
{
    public interface IUnitOfWork<TContext> : IDisposable where TContext : DbContext
    {
        IRepository<Entity> GetRepository<T>() where T : class;

        Task<int> Commit();

        void Rollback();
    }
}
