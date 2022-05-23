using Microsoft.EntityFrameworkCore;
using System;
using TaskManager.DataStore.Contract;

namespace TaskManager.DataStore
{
    public class TaskManagerContext:DbContext
    {
        /// <summary>
        ///     Designated constructor
        /// </summary>
        /// <param name="options">db context options</param>
        public TaskManagerContext(DbContextOptions<TaskManagerContext> options)
            : base(options)
        {
            // nop
        }

        /// <summary>
        ///     Book store in Database
        /// </summary>
        public DbSet<TaskData> TaskStores { get; set; }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<TaskData>();
        }
    }
}
