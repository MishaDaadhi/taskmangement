using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.DataStore.Contract;

namespace TaskManager.DataStore.TaskStore
{
    public class TaskStoreAddDataStoreService : ITaskStoreAddDataStoreService
    {
        /// <summary>
        ///     reader data store using entity framework db context
        /// </summary>
        private readonly ITaskManagerDataStore<TaskData> taskManagerDataStore;

        /// <summary>
        ///     Logger
        /// </summary>
        private readonly ILogger<TaskStoreAddDataStoreService> logger;

        public TaskStoreAddDataStoreService(ITaskManagerDataStore<TaskData> taskManagerDataStore, ILogger<TaskStoreAddDataStoreService> logger)
        {
            this.taskManagerDataStore = taskManagerDataStore;
            this.logger = logger;
        }

        public async Task<TaskData> ExecuteAsync(TaskData taskDetail, CancellationToken cancellationToken)
        {
            try
            {                
                return await this.taskManagerDataStore.AddAsync(taskDetail);
            }
            catch (Exception ex)
            {
                logger.LogError("Error has occured while adding Task Data {0}", ex);

            }
            return null;
        }
    }
}
