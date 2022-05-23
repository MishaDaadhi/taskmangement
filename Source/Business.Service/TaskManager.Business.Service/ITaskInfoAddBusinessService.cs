using TaskManager.Business.Model;
using TaskManager.Business.Service.Response;
using TaskManager.Core.Services;

namespace TaskManager.Business.Service
{
    public interface ITaskInfoAddBusinessService : IAsyncGenericService<TaskInfo, AddUpdateTaskResponse>
    {
    }
}
