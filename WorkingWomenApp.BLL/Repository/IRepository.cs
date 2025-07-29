using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WorkingWomenApp.Database.Core;
using Microsoft.EntityFrameworkCore.Query;
using WorkingWomenApp.Database.Models.WeatherApi;



namespace WorkingWomenApp.BLL.Repository
{
    public interface IRepository<T> where T : Entity
    {
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);

        Task<T> GetAsync(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false);
        Task<bool> AnyAsync(Expression<Func<T, bool>> filter, string? includeProperties = null);
        Task AddAsync(T model);
        Task AddRangeAsync(IEnumerable<T> models);
        Task UpdateAsync(T model);
        Task UpdateRangeAsync(IEnumerable<T> models);
        Task DeleteAsync(Guid id);
        Task DeleteAsync(T model);
        Task SoftDeleteAsync(Guid id);
        Task SoftDeleteAsync(T model);
        Task DeleteRangeAsync(IEnumerable<T> models);
        Task SoftDeleteRangeAsync(IEnumerable<T> models);
        Task ExecuteDeleteAsync(Expression<Func<T, bool>> condition);
        Task ExecuteUpdateAsync(Expression<Func<T, bool>> condition, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> updateExpression);
        Task ExecuteUpdateAsync(Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> updateExpression);

        Task TruncateTableAsync();

    }
}