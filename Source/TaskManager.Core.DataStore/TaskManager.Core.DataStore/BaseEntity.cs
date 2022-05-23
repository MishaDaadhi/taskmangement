using System;

namespace TaskManager.Core.DataStore
{
    /// <summary>
    ///     Base marker entity for all entities persisted in data store
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        ///     entity unique identifier
        /// </summary>
        public virtual Guid Id { get; set; }
    }
}
