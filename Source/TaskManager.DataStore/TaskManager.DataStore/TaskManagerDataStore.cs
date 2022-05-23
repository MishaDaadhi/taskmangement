using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Core.DataStore;

namespace TaskManager.DataStore
{
    /// <summary>
    ///    TaskManager data store common services
    /// </summary>
    /// <typeparam name="T">entity marker</typeparam>
    ///
    public class TaskManagerDataStore<T> : ITaskManagerDataStore<T>
         where T : BaseEntity
    {
        /// <summary>
        ///     TaskManager db context
        /// </summary>
        protected readonly TaskManagerContext dbContext;

        /// <summary>
        ///   Designated constructor
        /// </summary>
        /// <param name="dbContext"></param>
        public TaskManagerDataStore(TaskManagerContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<T> AddAsync(T entity)
        {
            await dbContext.Set<T>().AddAsync(entity);
            await dbContext.SaveChangesAsync();

            return entity;
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await dbContext.Set<T>().AsNoTracking<T>().ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<T> GetByIdAsync(Guid id)
        {
            return await dbContext.Set<T>().FindAsync(id);
        }

        /// <inheritdoc/>
        public async Task<T> UpdateAsync(T entity)
        {
            dbContext.Entry<T>(entity).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
            return entity; 
        }
    }
}
