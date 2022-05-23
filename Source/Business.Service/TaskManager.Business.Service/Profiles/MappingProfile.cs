using AutoMapper;
using TaskManager.Business.Model;
using TaskManager.DataStore.Contract;

namespace TaskManager.Business.Service.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TaskInfo, TaskData>().ReverseMap();
        }
    }
}
