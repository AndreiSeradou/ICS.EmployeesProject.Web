using ICS.EmployeesProject.BL.DTOs.Request;
using ICS.EmployeesProject.BL.Interfaces.Services;
using ICS.EmployeesProject.DAL.Models;
using ICS.EmployeesProject.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ICS.EmployeesProject.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmployeeService _employeeService;

        public HomeController(ILogger<HomeController> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            var result = _employeeService.Update(new EmployeeRequest { Id = 1, Name = "Valik", Surname = "Seredov", Position = ".net developer", YearOfBirth = 2000, Salary = 200 });

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