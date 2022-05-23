using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Business.Model;
using TaskManager.Business.Service;
using TaskManager.Business.Service.Response;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateTaskController : ControllerBase
    {
        private readonly ITaskInfoUpdateBusinessService taskInfoUpdateBusinessService;

        public UpdateTaskController(ITaskInfoUpdateBusinessService taskInfoUpdateBusinessService)
        {
            this.taskInfoUpdateBusinessService = taskInfoUpdateBusinessService;

        }
                
        [HttpPost]
        public async Task<AddUpdateTaskResponse> Post([FromBody] TaskInfo taskinfo, CancellationToken token)
        {
            if (ModelState.IsValid)
            {
                return await this.taskInfoUpdateBusinessService.ExecuteAsync(taskinfo, token);
            }

            return new AddUpdateTaskResponse() { Success = false, Message = "Invalid data passed." };
        }
    }
}
