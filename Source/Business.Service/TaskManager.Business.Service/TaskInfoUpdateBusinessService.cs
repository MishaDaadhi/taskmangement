using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Business.Model;
using TaskManager.Business.Service.Response;
using TaskManager.DataStore.Contract;
using TaskManager.DataStore.TaskStore;

namespace TaskManager.Business.Service
{
    public class TaskInfoUpdateBusinessService : ITaskInfoUpdateBusinessService
    {
        private readonly ITaskStoreUpdateDataStoreService taskupdateDataStoreService;
        private readonly ITaskPriorityandDueDateValidation taskPriorityandDueDateValidation;
        private readonly IMapper mapper;
        /// <summary>
        ///     Logger
        /// </summary>
        private readonly ILogger<TaskInfoUpdateBusinessService> logger;

        public TaskInfoUpdateBusinessService(ITaskStoreUpdateDataStoreService taskupdateDataStoreService, ITaskPriorityandDueDateValidation taskPriorityandDueDateValidation, IMapper mapper, ILogger<TaskInfoUpdateBusinessService> logger) {
            this.taskupdateDataStoreService = taskupdateDataStoreService;
            this.taskPriorityandDueDateValidation = taskPriorityandDueDateValidation;
            this.mapper = mapper;
            this.logger = logger;
        }
        public async Task<AddUpdateTaskResponse>ExecuteAsync(TaskInfo input, CancellationToken cancellationToken)
        {
            try
            {
                var newtask = this.mapper.Map<TaskData>(input);
               
                if (await taskPriorityandDueDateValidation.ExecuteAsync(newtask, cancellationToken) && input.Id != Guid.Empty)
                {
                    var result = await taskupdateDataStoreService.ExecuteAsync(newtask, cancellationToken);

                    if(result !=null)
                    return new AddUpdateTaskResponse() { Success = true, Message = "Task updated", TaskDto = this.mapper.Map<TaskInfo>(result) };
                }

                return new AddUpdateTaskResponse() { Success = false, Message = "Validation failed" };
            }
            catch (Exception ex)
            {
                logger.LogError($"Error thrown on updating task with id {input.Id}", ex);
                return new AddUpdateTaskResponse() { Success = false, Message = "Task added" };
            }
        }
    }
}
