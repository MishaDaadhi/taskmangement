using Microsoft.AspNetCore.Mvc;
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
                return await taskInfoAddBusinessService.ExecuteAsync(BuildTaskInfo(newtask), token);
            }

            return new AddUpdateTaskResponse() { Success = false, Message = "Invalid data passed." };
        }

        private TaskInfo BuildTaskInfo(AddTaskRequest newtask)
        {
            return new TaskInfo()
            {
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
