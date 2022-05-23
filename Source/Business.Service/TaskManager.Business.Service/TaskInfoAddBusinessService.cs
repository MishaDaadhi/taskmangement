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
    public class TaskInfoAddBusinessService: ITaskInfoAddBusinessService
    {
        private readonly ITaskStoreAddDataStoreService taskMangerDataStoreService;
        private readonly ITaskPriorityandDueDateValidation taskPriorityandDueDateValidation;
        private readonly IMapper mapper;

        /// <summary>
        ///     Logger
        /// </summary>
        private readonly ILogger<TaskInfoAddBusinessService> logger;

        public TaskInfoAddBusinessService(ITaskStoreAddDataStoreService taskMangerDataStoreService, ITaskPriorityandDueDateValidation taskPriorityandDueDateValidation, IMapper mapper, 
                      ILogger<TaskInfoAddBusinessService> logger)
        {           
            this.taskPriorityandDueDateValidation = taskPriorityandDueDateValidation;
            this.mapper = mapper;
            this.taskMangerDataStoreService = taskMangerDataStoreService;
        }

        public async Task<AddUpdateTaskResponse> ExecuteAsync(TaskInfo input, CancellationToken cancellationToken)
        {
            try
            {
                var newtask = this.mapper.Map<TaskData>(input);
                if (await taskPriorityandDueDateValidation.ExecuteAsync(newtask, cancellationToken) && input.Id == Guid.Empty)
                {
                    var result = await taskMangerDataStoreService.ExecuteAsync(newtask, cancellationToken);

                    return new AddUpdateTaskResponse() { Success = true, Message = "Task added", TaskDto = this.mapper.Map<TaskInfo>(result) };
                }
                return new AddUpdateTaskResponse() { Success = false, Message = "Validation failed" };
            }
            catch (Exception ex)
            {
                logger.LogError($"Error thrown on adding task - {input.Name}", ex);
                return new AddUpdateTaskResponse() { Success = false, Message = "Task is not added" };
            }
        }
    }
}
