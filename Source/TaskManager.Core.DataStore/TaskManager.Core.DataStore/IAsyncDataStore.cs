using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TaskManager.Core.DataStore
{   

    /// <summary>
    ///     interface for all data store repositories interacting with target data store
    /// </summary>
    /// <typeparam name="T">type to be persisited</typeparam>
    /// <remarks>adapted from olinestore sample NET reference architecture implemation</remarks>
    public interface IAsyncDataStore<T>
        where T : BaseEntity
    {
        /// <summary>
        ///     Get entity by unique identifier
        /// </summary>
        /// <param name="id">entity unique identifier</param>
        /// <param name="token">cancellation token</param>
        /// <returns>entity</returns>
        Task<T> GetByIdAsync(Guid id);

        /// <summary>
        ///     Gets all entities
        /// </summary>
        /// <param name="token">cancellation token</param>
        /// <returns>list of entity</returns>
        Task<IReadOnlyList<T>> GetAllAsync();

        /// <summary>
        ///     Adds entity to data store
        /// </summary>
        /// <param name="entity">entity to be added</param>
        /// <param name="token">cancellation token</param>
        /// <returns>entity added to datastore</returns>
        Task<T> AddAsync(T entity);

        /// <summary>
        ///     Updates entity
        /// </summary>
        /// <param name="entity">entity to be updated</param>
        /// <param name="token">Cancellation token</param>
        /// <returns>>entity updated in datastore</returns>
        Task<T> UpdateAsync(T entity);

        /// <summary>
        ///     Deletes entity from data store
        /// </summary>
        /// <param name="entity">entity to be deleted</param>
        /// <param name="token">cancellation token</param>
        /// <returns>awaitable task</returns>
        Task DeleteAsync(T entity);
    }
}
