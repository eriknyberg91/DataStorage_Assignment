using System.Diagnostics;
using System.Linq.Expressions;
using Data.Contexts;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public abstract class BaseRepository<TEntity>(DataContext context) : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly DataContext _context = context;
        private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            if (entity == null)
            {
                return null!;
            }

            try
            {
                await _context.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }

            catch (Exception exception)
            {
                Debug.WriteLine($"Encountered error creating new {nameof(TEntity)}: {exception.Message}");
                return null!;
            }
        }
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            if (expression == null)
            {
                return null!;
            }

            return await _dbSet.FirstOrDefaultAsync(expression) ?? null!;
        }

        public virtual async Task<TEntity> UpdateAsync(Expression<Func<TEntity, bool>> expression, TEntity updatedEntity)
        {
            if (updatedEntity == null)
            {
                return null!;
            }

            try
            {
                var existingEntity = await _dbSet.FirstOrDefaultAsync(expression) ?? null!;
                if (existingEntity == null)
                {
                    return null!;
                }

                _context.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);
                await _context.SaveChangesAsync();
                return updatedEntity;
            }
            catch (Exception exception)
            {
                Debug.WriteLine($"Error updating entity: {nameof(TEntity)}: {exception.Message}");
                return null!;
            }
        }

        public virtual async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression, TEntity updatedEntity)
        {
            if (expression == null)
            {
                return false;
            }
            try
            {
                var entity = await _dbSet.FirstOrDefaultAsync(expression) ?? null!;
                if (entity != null)
                {
                    _dbSet.Remove(entity);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine($"Error removing entity: {nameof(TEntity)}: {exception.Message}");
                return false;
            }
            return false;
        }
    }
}
