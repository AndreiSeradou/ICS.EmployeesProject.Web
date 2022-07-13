using ICS.EmployeesProject.DAL.Interfaces.Repositories;
using ICS.EmployeesProject.DAL.Models;
using ICS.EmployeesProject.DAL.Repositories;
using ICS.EmployeesProject.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ICS.EmployeesProject.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmployeeRepository _employeeRepository;

        public HomeController(ILogger<HomeController> logger, IEmployeeRepository employeeRepository)
        {
            _logger = logger;
            _employeeRepository = employeeRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            var result = _employeeRepository.Update(new Employee { Id = 2, Name = "Maksim", Surname = "Seredov", Position = ".net developer", YearOfBirth = 2000, Salary = 200 });

            var cheackResult = result;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}