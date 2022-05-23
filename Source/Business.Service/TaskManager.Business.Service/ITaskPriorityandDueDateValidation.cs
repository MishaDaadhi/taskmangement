using TaskManager.Core.Services;
using TaskManager.DataStore.Contract;

namespace TaskManager.Business.Service
{
    public interface ITaskPriorityandDueDateValidation : IAsyncGenericService<TaskData, bool>
    {
    }
}
