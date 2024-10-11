using AutoMapper;
using Demo.BLL.Models.Employees;
using Demo.BLL.Services.Employees;
using Demo.PL.ViewModels.Employees;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
	[Authorize]
	public class EmployeeController : Controller
    {
        #region Services
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IMapper _map;
        private readonly IWebHostEnvironment _env;
        public EmployeeController(
                IEmployeeService employeeService,
                ILogger<EmployeeController> logger,
                IMapper map,
                IWebHostEnvironment env
            )
        {
            _employeeService = employeeService;
            _logger = logger;
            _map = map;
            _env = env;
        }
        #endregion
        #region Index
        [HttpGet] // Get: Employee/Index
        public async Task<IActionResult> Index(string name)
        {
            var employees = await _employeeService.GetEmployeesAsync(name);
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeEditCreateViewModel employeeVM)
        {
            if (!ModelState.IsValid)
            {
                return View(employeeVM);
            }
            var Message = string.Empty;

            try
            {
                var CreatedEmp = _map.Map<CreatedEmployeeDto>(employeeVM);
                var result = await _employeeService.CreateEmployeeAsync(CreatedEmp);

                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    Message = "Employee Is Not Created";
                    ModelState.AddModelError(string.Empty, Message);
                    return View(employeeVM);
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
            return View(employeeVM);
        }

        #endregion
        #region Details
        [HttpGet] // Get: Employee/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
                return BadRequest();
            var employee = await _employeeService.GetEmployeeByIdAsync(id.Value);

            if (employee is null)
                return NotFound();
            return View(employee);
        }
        #endregion
        #region Update

        [HttpGet] // Get: Department/Edit/id
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return BadRequest();
            var employee = await _employeeService.GetEmployeeByIdAsync(id.Value);

            if (employee is null)
                return NotFound();
            var employeeVM = _map.Map<EmployeeEditCreateViewModel>(employee);

            return View(employeeVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute]int id, EmployeeEditCreateViewModel employeeVM)
        {
            if(!ModelState.IsValid)
                return View(employeeVM);
            var Message = string.Empty;
            try
            {
                var UpdatedEmp = new UpdatedEmployeeDto
                {
                    Id = id,
                    Name = employeeVM.Name,
                    Email = employeeVM.Email,
                    Address = employeeVM.Address,
                    Age= employeeVM.Age,
                    Salary = employeeVM.Salary,
                    PhoneNumber = employeeVM.PhoneNumber,
                    IsActive = employeeVM.IsActive,
                    EmployeeType = employeeVM.EmployeeType,
                    Gender = employeeVM.Gender,
                    HiringDate= employeeVM.HiringDate,
                };
                var Updated = await _employeeService.UpdateEmployeeAsync(UpdatedEmp) > 0;

                if (Updated)
                    return RedirectToAction(nameof(Index));
                Message = "An Error has occured during the Updating";
            }
            catch (Exception ex)
            {
                // 1.Log The Error
                _logger.LogError(ex,ex.Message);

                // 2.Set The Message
                Message = _env.IsDevelopment() ? ex.Message : "An Error has occured during the Updating";
            }
            ModelState.AddModelError(string.Empty, Message);
            return View(employeeVM);
        }

        #endregion
        #region Delete
        [HttpGet]  // Get: Employee/Delete/id?
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
                return BadRequest();
            var employee = await _employeeService.GetEmployeeByIdAsync(id.Value);

            if (employee is null)
                return NotFound();
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var Message = string.Empty;
            try
            {
                var deleted = await _employeeService.DeleteEmployeeAsync(id);

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
