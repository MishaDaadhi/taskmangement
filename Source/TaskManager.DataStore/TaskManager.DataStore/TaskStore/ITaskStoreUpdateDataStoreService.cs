using TaskManager.DataStore.Contract;

namespace TaskManager.DataStore.TaskStore
{
    public interface ITaskStoreUpdateDataStoreService : IDataStoreService<TaskData, TaskData>
    {
    }
}
