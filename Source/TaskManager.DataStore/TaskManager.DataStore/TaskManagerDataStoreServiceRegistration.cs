using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using TaskManager.DataStore.TaskStore;
using Microsoft.Extensions.Configuration;

namespace TaskManager.DataStore
{
    public static class TaskManagerDataStoreServiceRegistration
    {
        public static IServiceCollection AddDataStoreServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<TaskManagerContext>(options => options.UseSqlServer(configuration.GetConnectionString("TaskManagerConnectionString")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
            services.AddDbContext<TaskManagerContext>(options => options.UseCosmos(configuration["Endpoint"], configuration["PrimaryKey"], configuration["DBName"]));
            services.AddScoped(typeof(ITaskManagerDataStore<>), typeof(TaskManagerDataStore<>));
            services.AddScoped(typeof(ITaskStoreGetAllDataStoreService), typeof(TaskStoreGetAllDataStoreService));
            services.AddScoped(typeof(ITaskStoreAddDataStoreService), typeof(TaskStoreAddDataStoreService));
            services.AddScoped(typeof(ITaskStoreUpdateDataStoreService), typeof(TaskStoreUpdateDataStoreService));

            return services;
        }
    }
}
