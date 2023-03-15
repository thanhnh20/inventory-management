using Library.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repository.RepositoryImpl
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly InventoryManagementContext _context;
        protected DbSet<TEntity> _entities;
        public GenericRepository(InventoryManagementContext context)
        {
            _context = context;
            _entities = _context.Set<TEntity>();
        }
        public virtual async Task<bool> Add(TEntity entity)
        {
            try
            {
                _entities.Add(entity);
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;
        }

        public virtual async Task<bool> Delete(TEntity entity)
        {
            try
            {
                _entities.Remove(entity);
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;
        }

        public virtual async Task<IEnumerable<TEntity>> GetMany(Expression<Func<TEntity, bool>>? expression = null)
        {
            try
            {
                var filter = _entities.AsNoTracking();
                if (expression != null)
                {
                    filter = filter.Where(expression);
                }
                return await filter.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        public virtual async Task<TEntity> GetOne(Expression<Func<TEntity, bool>>? expression = null)
        {
            try
            {
                var filter = _entities.AsNoTracking();
                if (expression != null)
                {
                    filter = filter.Where(expression);
                }
                return await filter.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        public virtual async Task<bool> Update(TEntity entity)
        {
            try
            {
                _entities.Update(entity);
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;
        }
    }
}
