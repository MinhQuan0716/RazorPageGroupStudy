using Application.Commons;
using Application.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Infracstuructures.Repositories
{
    public class GenericRepo<TModel> : IGenericRepo<TModel> where TModel : class
    {
        protected DbSet<TModel> _dbSet;

        public GenericRepo(AppDbContext context)
        {
            _dbSet = context.Set<TModel>();
        }
        public async Task AddAsync(TModel model)
        {
            await _dbSet.AddAsync(model);
        }

        public async Task AddRangeAsync(List<TModel> models)
        {
            await _dbSet.AddRangeAsync(models);
        }

        public Task<List<TModel>> GetAllAsync() => _dbSet.ToListAsync();

        public async Task<List<TModel>> GetAllAsync(Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>>? include = null)
        {
            IQueryable<TModel> query = _dbSet;
            if (include != null)
            {
                query = include(query);
            }

            return await query.ToListAsync();
        }

        public async Task<TModel?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        public async Task RemoveAsync(TModel model)
        {
            _dbSet.Remove(model);
        }

        public async Task<Pagination<TModel>> ToPaginationAsync(int pageIndex = 0, int pageSize = 10)
        {
            // get total count of items in the db set
            var itemCount = await _dbSet.CountAsync();

            // Create Pagination instance
            // to set data related to paging
            // Calculate and replace pageIndex and pageSize
            // if they are invalid
            var result = new Pagination<TModel>()
            {
                PageSize = pageSize,
                TotalItemsCount = itemCount,
                PageIndex = pageIndex,
            };

            // Take items according to the page size and page index
            // skip items in the previous pages
            // and take next items equal to page size
            var items = await _dbSet.Skip(result.PageIndex * result.PageSize)
                .Take(result.PageSize)
                .AsNoTracking()
                .ToListAsync();

            // Assign items to page
            result.Items = items;

            return result;
        }

        public void Update(TModel model)
        {
            _dbSet.Update(model);
        }

        public void UpdateRange(List<TModel> models)
        {
            _dbSet.UpdateRange(models);
        }
    }
}
