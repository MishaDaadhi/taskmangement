using Microsoft.AspNetCore.Mvc;
using Polly;
using System;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Api.Contracts.Request;
using TaskManager.Business.Model;
using TaskManager.Business.Service;
using TaskManager.Business.Service.Response;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddTaskController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ITaskInfoAddBusinessService taskInfoAddBusinessService;

        public AddTaskController(ITaskInfoAddBusinessService taskInfoAddBusinessService) {
            this.taskInfoAddBusinessService = taskInfoAddBusinessService;

        }        
        
        [HttpPost]
        public async Task<AddUpdateTaskResponse> Post([FromBody] AddTaskRequest newtask, CancellationToken token)
        {
            if (ModelState.IsValid)
            {
                var polly = Policy
          .Handle<Exception>()
          .RetryAsync(3, (exception, retryCount, context) => Console.WriteLine($"try: {retryCount}, Exception: {exception.Message}"));

                var result = await polly.ExecuteAsync(async () => await taskInfoAddBusinessService.ExecuteAsync(BuildTaskInfo(newtask), token));

                return result; // await taskInfoAddBusinessService.ExecuteAsync(BuildTaskInfo(newtask), token);
            }

            return new AddUpdateTaskResponse() { Success = false, Message = "Invalid data passed." };
        }

        private TaskInfo BuildTaskInfo(AddTaskRequest newtask)
        {
            return new TaskInfo()
            {
                Id = Guid.NewGuid(),
                Name = newtask.Name,
                Description = newtask.Description,
                DueDate = newtask.DueDate,
                EndDate = newtask.EndDate,
                Priority = (TaskPriority) newtask.Priority,
                StartDate = newtask.StartDate,
                Status = (Status )newtask.Status
            };
        }
    }
}
