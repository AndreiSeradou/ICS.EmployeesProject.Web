using ICS.EmployeesProject.DAL.Models;

namespace ICS.EmployeesProject.DAL.Interfaces.Repositories
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();
        Employee Get(int id);
        bool Create(Employee model);
        bool Update(Employee model);
        bool Delete(int id);
    }
}
