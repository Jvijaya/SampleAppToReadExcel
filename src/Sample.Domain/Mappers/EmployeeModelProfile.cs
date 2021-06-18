using AutoMapper;
using Sample.Domain.Entities;
using Sample.Domain.Models;

namespace Sample.Domain.Mappers
{
    public class EmployeeModelProfile : Profile
    {
        public EmployeeModelProfile()
        {
            CreateMap<Employee, EmployeeModel>().ReverseMap();
        }
    }
}
