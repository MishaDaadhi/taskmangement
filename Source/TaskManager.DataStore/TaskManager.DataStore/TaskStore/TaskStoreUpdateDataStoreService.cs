using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.DataStore.Contract;

namespace TaskManager.DataStore.TaskStore
{
    public class TaskStoreUpdateDataStoreService : ITaskStoreUpdateDataStoreService
    {
        /// <summary>
        ///     task manager data store using entity framework db context
        /// </summary>
        private readonly ITaskManagerDataStore<TaskData> taskManagerDataStore;

        /// <summary>
        ///     Logger
        /// </summary>
        private readonly ILogger<TaskStoreUpdateDataStoreService> logger;

        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="taskManagerDataStore"></param>
        /// <param name="logger"></param>
        /// <param name="taskPriorityandDueDateCheck"></param>
        public TaskStoreUpdateDataStoreService(ITaskManagerDataStore<TaskData> taskManagerDataStore, ILogger<TaskStoreUpdateDataStoreService> logger)
        {
            this.taskManagerDataStore = taskManagerDataStore;
            this.logger = logger;
        }

        public async Task<TaskData> ExecuteAsync(TaskData input, CancellationToken cancellationToken)
        {
            var existingTask = await this.taskManagerDataStore.GetByIdAsync(input.Id);
            if (existingTask == null)
            {
                logger.LogError("Task with ID {0} doesn't Exist", input.Id);
                return null;
            }

            try
            {
                return await this.taskManagerDataStore.UpdateAsync(input);
            }
            catch (Exception ex)
            {
                logger.LogError("Error has occured while adding Task Data {0}", ex);
            }

            return null;
        }
    }
}
