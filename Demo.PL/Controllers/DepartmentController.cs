using Demo.BLL.Services.Departments;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentsService;

        public DepartmentController(IDepartmentService departmentsService)
        {
            _departmentsService = departmentsService;
        }

        public IActionResult Index()
        {
            var department = _departmentsService.GetAllDepartments();
            return View(department);
        }
    }
}
