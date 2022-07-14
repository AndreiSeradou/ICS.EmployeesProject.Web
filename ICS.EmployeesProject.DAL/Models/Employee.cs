namespace ICS.EmployeesProject.DAL.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Position { get; set; }
        public int YearOfBirth { get; set; }
        public int Salary { get; set; }
    }
}
