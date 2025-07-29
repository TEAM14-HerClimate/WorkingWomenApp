using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using WorkingWomenApp.BLL.UnitOfWork;
using WorkingWomenApp.Data;
using WorkingWomenApp.Database.Core;

namespace WorkingWomenApp.BLL.Repository
{
    public class Repository<T> : IRepository<T> where T  : Entity
    {

        private readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _model;
        private ApplicationDbContext appDbContext;
        
        private ApplicationDbContext dbContext;

        public Repository(ApplicationDbContext context)
        {
            this._context = context;
            _model = _context.Set<T>();
        }

        //public Repository(ApplicationDbContext appDbContext)
        //{
        //    this.appDbContext = appDbContext;
        //    this.unitOfWork = unitOfWork;
        //}

        //public Repository(ApplicationDbContext dbContext)
        //{
        //    this.dbContext = dbContext;
        //}
        public async Task<T> GetAsync(Expression<Func<T, bool>> filter, string? includeProperties = null,
            bool tracked = false)
        {
            IQueryable<T> query = tracked ? _model : _model.AsNoTracking();

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            // Ensure the filter is applied to the query
            query = query.Where(filter);

            // Use FirstOrDefaultAsync which is inherently asynchronous
            return await query.FirstOrDefaultAsync();
        }
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = _model;
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            // Use the AnyAsync method which is inherently asynchronous
            return await query.AnyAsync(filter);
        }
        public async Task<T> GetById(Guid id)
        {
            return await _model.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
        {
            IQueryable<T> query = _model;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.ToListAsync();
        }

        public async Task AddAsync(T model)
        => await _model.AddAsync(model);

        public async Task AddRangeAsync(IEnumerable<T> models)
        => await _model.AddRangeAsync(models);

        public async Task DeleteAsync(Guid id)
        {
            T? model = await _context.FindAsync<T>(id);
            if (model != null)
                await DeleteAsync(model);
        }

        public async Task DeleteAsync(T model)
        {
            if (_context.Entry(model).State is EntityState.Detached)
                _context.Attach(model);

            _model.Remove(model);
        }

        public async Task SoftDeleteAsync(Guid id)
        {
            T? model = await _context.FindAsync<T>(id);
            if (model is not null)
                await SoftDeleteAsync(model);
        }

        public async Task SoftDeleteAsync(T model)
        {
            _model.Remove(model);
            await UpdateAsync(model);
        }

        public async Task DeleteRangeAsync(IEnumerable<T> models)
        => _model.RemoveRange(models);

        public async Task SoftDeleteRangeAsync(IEnumerable<T> models)
        {
            foreach (T model in models)
            {
                await SoftDeleteAsync(model);
            }

        }

        public async Task UpdateAsync(T model)
        {
            _context.Attach(model);
            _context.Entry(model).State = EntityState.Modified;
        }

        public async Task UpdateRangeAsync(IEnumerable<T> models)
        => _model.UpdateRange(models);

        public async Task ExecuteDeleteAsync(Expression<Func<T, bool>> condition)
        {
            await _model.Where(condition)
                  .ExecuteDeleteAsync();
        }

        public async Task ExecuteUpdateAsync(Expression<Func<T, bool>> condition,
          Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> updateExpression)
        {
            await _model.Where(condition)
              .ExecuteUpdateAsync(updateExpression);
        }

        public async Task ExecuteUpdateAsync(Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> updateExpression)
        {
            await _model.ExecuteUpdateAsync(updateExpression);
        }

        public async Task TruncateTableAsync()
        {
            var tableName = _context.Model.FindEntityType(typeof(T)).GetTableName();
            await _context.Database.ExecuteSqlRawAsync($"TRUNCATE TABLE {tableName}");
        }


    }
}
