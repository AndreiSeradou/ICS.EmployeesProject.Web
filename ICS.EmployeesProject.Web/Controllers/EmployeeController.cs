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

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public IActionResult Index()
        {
            var employees = _employeeService.GetAll();

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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
