using System.Collections.Generic;
using TaskManager.Business.Model;

namespace TaskManager.Business.Service.Response
{
    public class AllTasksResponse : BaseResponse
    {
        public IEnumerable<TaskInfo> AllTasks { get; set; }
    }
}