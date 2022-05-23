using TaskManager.Core.DataStore;

namespace TaskManager.DataStore
{
    public interface ITaskManagerDataStore<T>: IAsyncDataStore<T>
        where T : BaseEntity
    {
    }
}
