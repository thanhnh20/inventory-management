using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<bool> Add(TEntity entity);
        Task<bool> Update(TEntity entity);
        Task<bool> Delete(TEntity entity);

        Task<TEntity> GetOne(Expression<Func<TEntity, bool>>? expression = null);
        Task<IEnumerable<TEntity>> GetMany(Expression<Func<TEntity, bool>>? expression = null);
    }
}
