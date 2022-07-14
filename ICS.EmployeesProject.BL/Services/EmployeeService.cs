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

        public IEnumerable<string> GetAllPositons()
        {
            var result = _employeeRepository.GetAll().Select(e => e.Position).Distinct();

            return result;
        }

        public List<object[]> GetAnAverageSalary(IEnumerable<string> positions)
        {
            var result = new List<object[]>()
            {
                    new object[positions.Count()]
            };

            int count = default;

            foreach (var el in positions)
            {
                var salary = _employeeRepository.GetAll().Where(e => e.Position == el).Select(e => e.Salary);

                if (salary is not null)
                {
                    double averageSalary = default;

                    foreach (var meaning in salary)
                    {
                        averageSalary += meaning;
                    }

                    averageSalary /= salary.Count();

                    result[0][count] = averageSalary;

                    count++;
                }
            }

            return result;
        }

        public bool Update(EmployeeRequest model)
        {
            var employeeDal = _mapper.Map<Employee>(model);

            var result = _employeeRepository.Update(employeeDal);

            return result;
        }
    }
}
