using Microsoft.EntityFrameworkCore;
using TaskManager.DataStore.Contract;

namespace TaskManager.DataStore
{
    public class TaskManagerContext:DbContext
    {
        /// <summary>
        /// Name of the partition key shadow property.
        /// </summary>
        public const string PartitionKey = nameof(PartitionKey);

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


          var taskData = builder.Entity<TaskData>()             
              .HasPartitionKey(t => t.PartitionKey)
              .HasDefaultTimeToLive(600)
              .HasNoDiscriminator()
              .HasKey(d => d.Id)
            //.Property<int>("TimeToLive")
            //.ToJsonProperty("ttl")
            ;

            builder.HasDefaultContainer("TaskManager");
            //builder.HasAutoscaleThroughput(2);
        }
    }
}
