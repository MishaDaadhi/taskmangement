using askManager.Core.Common;
using System.Collections.Generic;
using TaskManager.DataStore.Contract;

namespace TaskManager.DataStore.TaskStore
{
    public interface ITaskStoreGetAllDataStoreService: IDataStoreService<Null, IEnumerable<TaskData>>
    {
    }
}
