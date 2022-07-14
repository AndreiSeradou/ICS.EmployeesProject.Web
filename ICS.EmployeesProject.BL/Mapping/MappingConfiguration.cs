using AutoMapper;
using ICS.EmployeesProject.BL.DTOs.Request;
using ICS.EmployeesProject.BL.DTOs.Response;
using ICS.EmployeesProject.DAL.Models;

namespace ICS.EmployeesProject.BL.Mapping
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            CreateMap<Employee, EmployeeRequest>().ReverseMap();
            CreateMap<Employee, EmployeeResponse>().ReverseMap();
            CreateMap<EmployeeRequest, EmployeeResponse>().ReverseMap();
        }
    }
}
