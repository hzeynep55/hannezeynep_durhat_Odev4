using Homework4Dto;
using Homework4Data;
using AutoMapper;

namespace Homework4Service
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, PersonDto>().ReverseMap();
        }
    }
}
