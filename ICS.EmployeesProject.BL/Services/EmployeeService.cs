using AutoMapper;
using ICS.EmployeesProject.BL.DTOs.Request;
using ICS.EmployeesProject.BL.DTOs.Response;
using ICS.EmployeesProject.BL.Interfaces.Services;
using ICS.EmployeesProject.DAL.Interfaces.Repositories;
using ICS.EmployeesProject.DAL.Models;

namespace ICS.EmployeesProject.BL.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public bool Create(EmployeeRequest model)
        {
            var employeeDal = _mapper.Map<Employee>(model);

            var result = _employeeRepository.Create(employeeDal);

            return result;
        }

        public bool Delete(int id)
        {
            var result = _employeeRepository.Delete(id);

            return result;
        }

        public EmployeeResponse Get(int id)
        {
            var employeeDal = _employeeRepository.Get(id);

            var employeeResponse = _mapper.Map<EmployeeResponse>(employeeDal);

            return employeeResponse;
        }

        public IEnumerable<EmployeeResponse> GetAll()
        {
            var employeesDal = _employeeRepository.GetAll();

            var employeesResponseList = _mapper.Map<IEnumerable<EmployeeResponse>>(employeesDal);

            return employeesResponseList;
        }

        public bool Update(EmployeeRequest model)
        {
            var employeeDal = _mapper.Map<Employee>(model);

            var result = _employeeRepository.Update(employeeDal);

            return result;
        }
    }
}
