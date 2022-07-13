using ICS.EmployeesProject.DAL.Configuration;
using ICS.EmployeesProject.DAL.Interfaces.Repositories;
using ICS.EmployeesProject.DAL.Models;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;

namespace ICS.EmployeesProject.DAL.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ConnectionStrings _connectionStrings;

        public EmployeeRepository(IOptionsMonitor<ConnectionStrings> optionsMonitor)
        {
            _connectionStrings = optionsMonitor.CurrentValue;
        }

        public bool Create(Employee model)
        {
            var sqlExpression = "INSERT INTO Employees (Name, Surname, Position, YearOfBirth, Salary) VALUES (@Name, @Surname, @Position, @YearOfBirth, @Salary)";
            int result;

            using (var connection = new SqliteConnection(_connectionStrings.SqLiteConnectionString))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                SqliteParameter nameParam = new SqliteParameter("@Name", model.Name);
                SqliteParameter surnameParam = new SqliteParameter("@Surname", model.Surname);
                SqliteParameter positionParam = new SqliteParameter("@Position", model.Position);
                SqliteParameter yearOfBirthParam = new SqliteParameter("@YearOfBirth", model.YearOfBirth);
                SqliteParameter salaryParam = new SqliteParameter("@Salary", model.Salary);

                command.Parameters.Add(nameParam);
                command.Parameters.Add(surnameParam);
                command.Parameters.Add(positionParam);
                command.Parameters.Add(yearOfBirthParam);
                command.Parameters.Add(salaryParam);

                result = command.ExecuteNonQuery();
            }

            if (result < 1)
            {
                return false;
            }

            return true;
        }

        public bool Delete(int id)
        {
            var sqlExpression = $"DELETE  FROM Employees WHERE Id = @Id";
            int result;

            using (var connection = new SqliteConnection(_connectionStrings.SqLiteConnectionString))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                SqliteParameter idParam = new SqliteParameter("@Id", id);

                command.Parameters.Add(idParam);

                result = command.ExecuteNonQuery();
            }

            if (result < 1)
            {
                return false;
            }

            return true;
        }

        public Employee Get(int id)
        {
            Employee employee = default;
            var sqlExpression = "SELECT * FROM Employees WHERE Id = @Id";

            using (var connection = new SqliteConnection(_connectionStrings.SqLiteConnectionString))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                SqliteParameter idParam = new SqliteParameter("@Id", id);

                command.Parameters.Add(idParam);

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            employee = new Employee { Id = reader.GetInt32(0), Name = reader.GetString(1), Surname = reader.GetString(2), Position = reader.GetString(3), YearOfBirth = reader.GetInt32(4), Salary = reader.GetInt32(5) };
                        }
                    }
                }
            }

            return employee;
        }

        public IEnumerable<Employee> GetAll()
        {
            var employeeList = new List<Employee>();
            var sqlExpression = "SELECT * FROM Employees";

            using (var connection = new SqliteConnection(_connectionStrings.SqLiteConnectionString))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            employeeList.Add(new Employee { Id = reader.GetInt32(0), Name = reader.GetString(1), Surname = reader.GetString(2), Position = reader.GetString(3), YearOfBirth = reader.GetInt32(4), Salary = reader.GetInt32(5) });
                        }
                    }
                }
            }

            return employeeList;
        }

        public bool Update(Employee model)
        {
            var sqlExpression = "UPDATE Employees SET Name = @Name, Surname = @Surname, Position = @Position, YearOfBirth = @YearOfBirth, Salary = @Salary  WHERE Id = @Id";
            int result;

            using (var connection = new SqliteConnection(_connectionStrings.SqLiteConnectionString))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                SqliteParameter idParam = new SqliteParameter("@Id", model.Id);
                SqliteParameter nameParam = new SqliteParameter("@Name", model.Name);
                SqliteParameter surnameParam = new SqliteParameter("@Surname", model.Surname);
                SqliteParameter positionParam = new SqliteParameter("@Position", model.Position);
                SqliteParameter yearOfBirthParam = new SqliteParameter("@YearOfBirth", model.YearOfBirth);
                SqliteParameter salaryParam = new SqliteParameter("@Salary", model.Salary);

                command.Parameters.Add(idParam);
                command.Parameters.Add(nameParam);
                command.Parameters.Add(surnameParam);
                command.Parameters.Add(positionParam);
                command.Parameters.Add(yearOfBirthParam);
                command.Parameters.Add(salaryParam);

                result = command.ExecuteNonQuery();
            }

            if (result < 1)
            {
                return false;
            }

            return true;
        }
    }
}
