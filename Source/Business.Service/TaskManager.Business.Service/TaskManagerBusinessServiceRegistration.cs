using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace TaskManager.Business.Service
{
    public static class TaskManagerBusinessServiceRegistration
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped(typeof(ITaskInfoAddBusinessService), typeof(TaskInfoAddBusinessService));
            services.AddScoped(typeof(ITaskInfoUpdateBusinessService), typeof(TaskInfoUpdateBusinessService));
            services.AddScoped(typeof(ITaskPriorityandDueDateValidation), typeof(TaskPriorityandDueDateValidation));
            services.AddScoped(typeof(ITaskInfoGetTasksBusinessService), typeof(TaskInfoGetTasksBusinessService));

            return services;
        }
    }
}
