using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Business.Service;
using TaskManager.Business.Service.Response;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllTasksController : ControllerBase
    {
        /// <summary>
        /// get all tasks service
        /// </summary>
        private readonly ITaskInfoGetTasksBusinessService taskInfoGetTasksBusinessService;
        private readonly ILogger<AllTasksController> logger;
        public AllTasksController(ITaskInfoGetTasksBusinessService taskInfoGetTasksBusinessService, ILogger<AllTasksController> logger)
        {
           this.taskInfoGetTasksBusinessService = taskInfoGetTasksBusinessService;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<AllTasksResponse> Get(CancellationToken token)
        {
<<<<<<< HEAD
            logger.LogInformation("----- Getting all Tasks");
            return await this.taskInfoGetTasksBusinessService.ExecuteAsync(null, token);
=======
         var result = await this.taskInfoGetTasksBusinessService.ExecuteAsync(null, token);
            return Ok(result);
>>>>>>> 51d348520276f68d966a31621903e4720bc9f4ca
        }
    }
}
