using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
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
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ApplicationDbContext _appDbContext;

        public UnitOfWork( ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            //_appDbContext = appDbContext;
        }

     
        public IRepository<Entity> GetRepository<T>() where T : class
        {
            return new Repository<Entity>(_dbContext);
        }

        public async Task<int> Commit()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Rollback()
        {
            _dbContext.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        }

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }
    }
}
