using System.Linq.Expressions;
using Data.Entities;

namespace Data.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> CreateAsync(TEntity entity);
        Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression, TEntity updatedEntity);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> UpdateAsync(Expression<Func<TEntity, bool>> expression, TEntity updatedEntity);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression);
    }
}