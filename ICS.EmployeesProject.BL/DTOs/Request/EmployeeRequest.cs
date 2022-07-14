using System.ComponentModel.DataAnnotations;

namespace ICS.EmployeesProject.BL.DTOs.Request
{
    public class EmployeeRequest
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        public int YearOfBirth { get; set; }
        [Required]
        public int Salary { get; set; }
    }
}
