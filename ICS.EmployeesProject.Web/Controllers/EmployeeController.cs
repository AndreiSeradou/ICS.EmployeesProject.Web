using ICS.EmployeesProject.BL.DTOs.Request;
using ICS.EmployeesProject.BL.Interfaces.Services;
using ICS.EmployeesProject.Configuration;
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

        public IActionResult Index(string filterByPosition)
        {
            try
            {
                var employees = _employeeService.GetAll();

                if (!string.IsNullOrEmpty(filterByPosition))
                {
                    employees = employees.Where(e => e.Position.ToUpper().Contains(filterByPosition.ToUpper()));
                }

                return View(employees);
            }
            catch
            {
                return RedirectToAction(ApplicationConfiguration.ErrorAction);
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeRequest model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _employeeService.Create(model);

                    if (result is false)
                        return View();

                    return RedirectToAction(ApplicationConfiguration.IndexAction);
                }

                return View();
            }
            catch
            {
                return RedirectToAction(ApplicationConfiguration.ErrorAction);
            }
        }

        public IActionResult Edit(int id)
        {
            try
            {
                var employee = _employeeService.Get(id);

                return View(employee);
            }
            catch
            {
                return RedirectToAction(ApplicationConfiguration.ErrorAction);
            }
        }

        [HttpPost]
        public IActionResult Edit(EmployeeRequest model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _employeeService.Update(model);

                    if (result is false)
                        return View();

                    return RedirectToAction(ApplicationConfiguration.IndexAction);
                }

                return View();
            }
            catch
            {
                return RedirectToAction(ApplicationConfiguration.ErrorAction);
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                _employeeService.Delete(id);

                return RedirectToAction(ApplicationConfiguration.IndexAction);
            }
            catch
            {
                return RedirectToAction(ApplicationConfiguration.ErrorAction);
            }
        }

        public IActionResult Report()
        {
            try
            {
                var positions = _employeeService.GetAllPositons();

                var headerRow = new List<string[]>()
                {
                    positions.ToArray()
                };

                var cellData = _employeeService.GetAnAverageSalary(positions);

                _processService.OpenExcel(headerRow, cellData);

                return RedirectToAction(ApplicationConfiguration.IndexAction);
            }
            catch
            {
                return RedirectToAction(ApplicationConfiguration.ErrorAction);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
