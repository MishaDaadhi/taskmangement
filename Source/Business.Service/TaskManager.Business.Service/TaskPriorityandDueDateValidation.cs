using System;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Business.Model;
using TaskManager.DataStore.Contract;
using TaskManager.DataStore.TaskStore;
using TaskManager.Core.Common;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace TaskManager.Business.Service
{
    public class TaskPriorityandDueDateValidation: ITaskPriorityandDueDateValidation
    {
        /// <summary>
        ///  TaskStoreGetAllDataStoreService
        /// </summary>
        private readonly ITaskStoreGetAllDataStoreService taskStoreGetAllDataStoreService;

        /// <summary>
        ///     Logger
        /// </summary>
        private readonly ILogger<TaskPriorityandDueDateValidation> logger;

        public TaskPriorityandDueDateValidation(ITaskStoreGetAllDataStoreService taskStoreGetAllDataStoreService, ILogger<TaskPriorityandDueDateValidation> logger) {

            this.taskStoreGetAllDataStoreService = taskStoreGetAllDataStoreService;
            this.logger = logger;
        }

        public async Task<bool> ExecuteAsync(TaskData input, CancellationToken cancellationToken)
        {
            var existingTasks = await taskStoreGetAllDataStoreService.ExecuteAsync(null, cancellationToken);
            var currenttask = existingTasks?.FirstOrDefault(t => t.Id == input.Id);
            try
            {
                //validate new task with past due date   
                if (input.Id == Guid.Empty && input.DueDate.Date < DateTime.Now.Date)
                {
                    return false;
                }

                //validate task doesn't exist   
                if (input.Id != Guid.Empty && currenttask == null )
                {
                    return false;
                }

                if (input.Priority.ToEnum<TaskPriority>() == TaskPriority.High)
                {

                    var highPriorityTasks = existingTasks.Where(t => t.Priority.ToEnum<TaskPriority>() == TaskPriority.High &&
                         !(t.Status.ToEnum<Status>() == Status.Finished) && t.DueDate.Date == input.DueDate.Date);

                    if (highPriorityTasks.Count() >= 100)
                    {
                        //task can not be added or updated with high priority
                        if (highPriorityTasks.Where(t => t.Id == input.Id).Count() > 0)
                            return false;

                        //existing task due date can not be updated with high priority
                        else if (currenttask.DueDate != input.DueDate)
                            return false;

                        else
                            return true;
                    }
                }

            }
            catch (Exception ex)
            {
                logger.LogError("Error has occured while validating Task Data {0}", ex);
                return false;
            }

            return true;
        }        
    }
}