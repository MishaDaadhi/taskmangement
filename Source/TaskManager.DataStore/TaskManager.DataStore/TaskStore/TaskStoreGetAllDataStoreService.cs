using askManager.Core.Common;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.DataStore.Contract;

namespace TaskManager.DataStore.TaskStore
{
    public class TaskStoreGetAllDataStoreService : ITaskStoreGetAllDataStoreService
    {
        /// <summary>
        ///     reader data store using entity framework db context
        /// </summary>
        private readonly ITaskManagerDataStore<TaskData> taskManagerDataStore;

        public TaskStoreGetAllDataStoreService(ITaskManagerDataStore<TaskData> taskManagerDataStore)
        {
            this.taskManagerDataStore = taskManagerDataStore;            
        }
        public async Task<IEnumerable<TaskData>> ExecuteAsync(Null input, CancellationToken cancellationToken)
        {
            return await this.taskManagerDataStore.GetAllAsync();
        }
    }
}
