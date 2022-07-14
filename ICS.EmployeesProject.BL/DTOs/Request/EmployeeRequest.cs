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
        [Range(1960, 2022, ErrorMessage = "Only numbers in the range 1960 - 2022")]
        public int YearOfBirth { get; set; }
        [Required]
        [Range(0, 100000, ErrorMessage = "Only numbers in the range 0 - 100.000")]
        public int Salary { get; set; }
    }
}
