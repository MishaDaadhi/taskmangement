using TaskManager.Business.Model;

namespace TaskManager.Business.Service.Response
{
    public class AddUpdateTaskResponse: BaseResponse
    {

        public TaskInfo TaskDto { get; set; }
    }
}
