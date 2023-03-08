using askManager.Core.Common;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Business.Model;
using TaskManager.Business.Service.Response;
using TaskManager.DataStore.Contract;
using TaskManager.DataStore.TaskStore;

namespace TaskManager.Business.Service
{
    public class TaskInfoGetTasksBusinessService : ITaskInfoGetTasksBusinessService
    {
        /// <summary>
        ///  TaskStoreGetAllDataStoreService
        /// </summary>
        private readonly ITaskStoreGetAllDataStoreService taskStoreGetAllDataStoreService;

        /// <summary>
        ///     Logger
        /// </summary>
        private readonly ILogger<TaskPriorityandDueDateValidation> logger;

        /// <summary>
        ///  automapper
        /// </summary>
        private readonly IMapper mapper;

        public TaskInfoGetTasksBusinessService(ITaskStoreGetAllDataStoreService taskStoreGetAllDataStoreService, ILogger<TaskPriorityandDueDateValidation> logger, IMapper mapper)
        {
            this.taskStoreGetAllDataStoreService = taskStoreGetAllDataStoreService;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task<AllTasksResponse> ExecuteAsync(Null input, CancellationToken cancellationToken)
        {
            try
            {
                var existingTasks = await taskStoreGetAllDataStoreService.ExecuteAsync(null, cancellationToken);

                return new AllTasksResponse() { Success = true, Message = "All Tasks listed", AllTasks = mapper.Map<IEnumerable<TaskData>, List<TaskInfo>>(existingTasks) };
            }
            catch (Exception ex)
            {
                logger.LogError("Error thrown on getting list of tasks.", ex);
                return new AllTasksResponse() { Success = false, Message = "Task not found" };
            }
        }
    }
}
