using ICS.EmployeesProject.BL.DTOs.Request;
using ICS.EmployeesProject.BL.DTOs.Response;

namespace ICS.EmployeesProject.BL.Interfaces.Services
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeResponse> GetAll();
        IEnumerable<string> GetAllPositons();
        List<object[]> GetAnAverageSalary(IEnumerable<string> positions);
        EmployeeResponse Get(int id);
        bool Create(EmployeeRequest model);
        bool Update(EmployeeRequest model);
        bool Delete(int id);
    }
}
