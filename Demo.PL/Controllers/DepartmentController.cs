using Demo.BLL.Models.Departments;
using Demo.BLL.Services.Departments;
using Demo.PL.ViewModels.Departments;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace Demo.PL.Controllers
{
    public class DepartmentController : Controller
    {
        #region Services

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

        #endregion
        #region Index
        
        [HttpGet] // Get: Department/Index
        public IActionResult Index()
        {
            var department = _departmentsService.GetAllDepartments();
            return View(department);
        }

        #endregion
        #region Create
        
        [HttpGet] // Get: Department/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost] //Post
        [ValidateAntiForgeryToken]
        public IActionResult Create(DepartmentEditViewModel departmentVM)
        {
            if (!ModelState.IsValid) //Server Side Validation
            {
                return View(departmentVM);
            }
            var Message = string.Empty;
            try
            {
                var departmentToCreate = new CreatedDepartmentDto()
                {
                    Code = departmentVM.Code,
                    Name = departmentVM.Name,
                    Description = departmentVM.Description,
                    CreationDate = departmentVM.CreationDate,
                };
                var Result = _departmentsService.CreateDepartment(departmentToCreate);

                if (Result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    Message = "Department Is Not Created";
                    ModelState.AddModelError(string.Empty, Message);
                    return View(departmentVM);
                }
            }
            catch (Exception ex)
            {
                // 1. Log the Exception
                _logger.LogError(ex, ex.Message);

                // 2. Set Messaege
                Message = _env.IsDevelopment() ? ex.Message : "An Error has occured during the Updating";
            }
            ModelState.AddModelError(string.Empty, Message);
            return View(departmentVM);
        }

        #endregion
        #region Details

        [HttpGet] // Get: Department/Details
        public IActionResult Details(int? id)
        {
            if (id is null)
                return BadRequest();
            var department = _departmentsService.GetDartmentById(id.Value);

            if (department is null)
                return NotFound();
            return View(department);
        }

        #endregion
        #region Edit

        [HttpGet] // Get: Department/Edit/id
        public IActionResult Edit(int? id)
        {
            if (id is null)
                return BadRequest(); // 400
            var department = _departmentsService.GetDartmentById(id.Value);

            if (department is null)
                return NotFound();
            return View(new DepartmentEditViewModel
            {
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreationDate = department.CreationDate,
            });
        }

        [HttpPost] // Post
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, DepartmentEditViewModel departmentVM)
        {
            if (!ModelState.IsValid) //Server Side Validation
                return View(departmentVM);
            var Message = string.Empty;
            try
            {
                var departmentToUpdate = new UpdatedDepartmentDto()
                {
                    Id = id,
                    Code = departmentVM.Code,
                    Name = departmentVM.Name,
                    Description = departmentVM.Description,
                    CreationDate = departmentVM.CreationDate,
                };

                var Updated = _departmentsService.UpdatedDepartment(departmentToUpdate) > 0;
                if (Updated)
                    return RedirectToAction(nameof(Index));
                Message = "An Error has occured during the Updating";
            }
            catch (Exception ex)
            {
                // 1.Log the Error
                _logger.LogError(ex, ex.Message);

                // 2.Set the message
                Message = _env.IsDevelopment() ? ex.Message : "An Error has occured during the Updating";
            }
            ModelState.AddModelError(string.Empty, Message);
            return View(departmentVM);
        }

        #endregion
        #region Delete

        [HttpGet] // Get:Department/Delete
        public IActionResult Delete(int? id)
        {
            if (id is null)
                return BadRequest();

            var deleted = _departmentsService.GetDartmentById(id.Value);

            if (deleted is null)
                return NotFound();
            return View(deleted);
        }

        [HttpPost] //Post
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var message = string.Empty;
            try
            {
                var deleted = _departmentsService.DeleteDepartment(id);

                if (deleted)
                    return RedirectToAction(nameof(Index));
                message = "an error has occured during the deleting of the department";
            }
            catch (Exception ex)
            {

                // 1.Log the error
                _logger.LogError(ex, ex.Message);

                // 2.Set the message

                message = _env.IsDevelopment() ? ex.Message : "an error has occured during the deleting of the department";
            }
            //ModelState.AddModelError(string.Empty, message);
            return RedirectToAction(nameof(Index));
            
        }

        #endregion
    }
}
