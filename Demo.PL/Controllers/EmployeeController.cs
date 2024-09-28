using Demo.BLL.Models.Employees;
using Demo.BLL.Services.Employees;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace Demo.PL.Controllers
{
    public class EmployeeController : Controller
    {
        #region Services
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IWebHostEnvironment _env;
        public EmployeeController(
                IEmployeeService employeeService,
                ILogger<EmployeeController> logger,
                IWebHostEnvironment env
            )
        {
            _employeeService = employeeService;
            _logger = logger;
            _env = env;
        }
        #endregion

        #region Index
        [HttpGet] // Get: Employee/Index
        public IActionResult Index()
        {
            var employees = _employeeService.GetAllEmployees();
            return View(employees);
        }
        #endregion

        #region Create

        [HttpGet] // Get: Employee/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreatedEmployeeDto emoloyeeDto)
        {
            if (!ModelState.IsValid)
            {
                return View(emoloyeeDto);
            }
            var Message = string.Empty;

            try
            {
                var result = _employeeService.CreateEmployee(emoloyeeDto);

                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    Message = "Employee Is Not Created";
                    ModelState.AddModelError(string.Empty, Message);
                    return View(emoloyeeDto);
                }
            }
            catch (Exception ex)
            {
                // 1.Log The Error
                _logger.LogError(ex, ex.Message);

                // 2.Set Message
                Message = _env.IsDevelopment() ? ex.Message : "Employee Is Not Created";
            }
            ModelState.AddModelError(string.Empty, Message);
            return View(emoloyeeDto);
        }

        #endregion

        [HttpGet] // Get: Employee/Details
        #region Details
        public IActionResult Details(int? id)
        {
            if (id is null)
                return BadRequest();
            var employee = _employeeService.GetEmployeeById(id.Value);

            if (employee is null)
                return NotFound();
            return View(employee);
        }
        #endregion

        #region Update
        //[HttpGet] // Get: Department/Edit/id
        //public IActionResult Edit(int? id)
        //{
        //    if(id is null)
        //        return BadRequest();
        //    var employee = _employeeService.GetEmployeeById(id.Value);

        //    if(employee is null)
        //        return NotFound();
        //    return View(new );
        //}
        #endregion

        #region Delete
        [HttpGet]  // Get: Employee/Delete/id?
        public IActionResult Delete(int? id)
        {
            if (id is null)
                return BadRequest();
            var employee = _employeeService.GetEmployeeById(id.Value);

            if (employee is null)
                return NotFound();
            return View(employee);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var Message = string.Empty;
            try
            {
                var deleted = _employeeService.DeleteEmployee(id);

                if (deleted)
                    return RedirectToAction(nameof(Index));
                Message = "an error has occured during the deleting of the employee";
            }
            catch (Exception ex)
            {
                // 1.Log The Error
                _logger.LogError(ex,ex.Message);

                // 2.Set the Message

                Message = _env.IsDevelopment() ? ex.Message : "an error has occured during the deleting of the employee";
            }
            return RedirectToAction(nameof(Index));

        }
        #endregion
    }
}
