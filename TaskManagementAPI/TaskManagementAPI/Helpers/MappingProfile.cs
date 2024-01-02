using AutoMapper;
using TaskManagementAPI.Models.DTO;
using TaskManagementAPI.Models.Domain;

namespace TaskManagementAPI.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TaskModel, TaskDto>().ReverseMap();
        }
    }
}
