using ICS.EmployeesProject.BL.DTOs.Request;
using ICS.EmployeesProject.BL.Interfaces.Services;
using ICS.EmployeesProject.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ICS.EmployeesProject.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IProcessService _processService;

        public EmployeeController(IEmployeeService employeeService, IProcessService processService)
        {
            _employeeService = employeeService;
            _processService = processService;
        }

        public IActionResult Index(string filterBy)
        {
            var employees = _employeeService.GetAll();

            if (!String.IsNullOrEmpty(filterBy))
            {
                employees = employees.Where(e => e.Position.ToUpper().Contains(filterBy.ToUpper()));
            }

            return View(employees);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeRequest model)
        {
            if (ModelState.IsValid)
            {
                var result = _employeeService.Create(model);

                if (result is false)
                    return View();

                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Edit(int id)
        {
            var employee = _employeeService.Get(id);

            return View(employee);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeRequest model)
        {
            if (ModelState.IsValid)
            {
                var result = _employeeService.Update(model);

                if (result is false)
                    return View();

                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Delete(int id)
        {
            _employeeService.Delete(id);

            return RedirectToAction("Index");
        }

        public IActionResult Report()
        {
            var positions = _employeeService.GetAll().Select(e => e.Position).Distinct();

            var headerRow = new List<string[]>()
            {
                 positions.ToArray()
            };

            var cellData = new List<object[]>()
            {
                new object[positions.Count()]
            };

            int count = default;

            foreach (var el in positions)
            {
                var salary = _employeeService.GetAll().Where(e => e.Position == el).Select(e => e.Salary);

                if (salary is not null)
                {
                    double averageSalary = default;

                    foreach (var meaning in salary)
                    {
                        averageSalary += meaning;
                    }

                    averageSalary /= salary.Count();

                    cellData[0][count] = averageSalary;

                    count++;
                }
            }

            _processService.OpenExcel(headerRow, cellData);

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
