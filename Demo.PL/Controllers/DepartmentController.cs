using Demo.BLL.Models.Departments;
using Demo.BLL.Services.Departments;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace Demo.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentsService;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IWebHostEnvironment _env;

        public DepartmentController
            (
            IDepartmentService departmentsService,
            ILogger<DepartmentController> logger,
            IWebHostEnvironment env
            )
        {
            _logger = logger;
            _env = env;
            _departmentsService = departmentsService;
        }

        [HttpGet] // Get: Department/Index
        public IActionResult Index()
        {
            var department = _departmentsService.GetAllDepartments();
            return View(department);
        }

        [HttpGet] // Get: Department/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost] //Post: Department/Create
        public IActionResult Create(CreatedDepartmentDto department)
        {
            if(!ModelState.IsValid)
            {
                return View(department);
            }
            var Message = string.Empty;
            try
            {
                var Result = _departmentsService.CreateDepartment(department);

                if(Result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    Message = "Department Is Not Created";
                    ModelState.AddModelError(string.Empty, Message);
                    return View(department);
                }
            }
            catch(Exception ex)
            {
                // 1. Log the Exception
                _logger.LogError(ex,ex.Message);

                // 2. Set Messaege
                if (_env.IsDevelopment())
                {
                    Message = ex.Message;
                    return View(department);
                }
                else
                {
                    Message = "Department Is Not Created";
                    return View("Error",Message);
                }
            }
        }

        [HttpGet] // Get: Department/Create
        public IActionResult Details(int? id)
        {
            if(id is null)
                return BadRequest();
            var department = _departmentsService.GetDartmentById(id.Value);

            if(department is null)
                return NotFound();
            return View(department);
        }
    }
}
