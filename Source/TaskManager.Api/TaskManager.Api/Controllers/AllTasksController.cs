﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public AllTasksController(ITaskInfoGetTasksBusinessService taskInfoGetTasksBusinessService)
        {
           this.taskInfoGetTasksBusinessService = taskInfoGetTasksBusinessService;
        }

        [HttpGet]
        public async Task<AllTasksResponse> Get(CancellationToken token)
        {
         var result = await this.taskInfoGetTasksBusinessService.ExecuteAsync(null, token);
            return Ok(result);
        }
    }
}
