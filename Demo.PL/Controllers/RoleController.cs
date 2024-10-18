using AutoMapper;
using Demo.PL.ViewModels.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Demo.PL.Controllers
{
    public class RoleController : Controller
    {
        #region Services
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public RoleController(RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }
        #endregion
        #region Index
        public async Task<IActionResult> Index(string SearchValue)
        {
            if (string.IsNullOrEmpty(SearchValue))
            {
                var Roles = await _roleManager.Roles.ToListAsync();
                var MappedRoles = _mapper.Map<IEnumerable<RoleViewModel>>(Roles);
                return View(MappedRoles);
            }
            else
            {
                var Role = await _roleManager.FindByNameAsync(SearchValue);
                var MappedRoles = _mapper.Map<IEnumerable<RoleViewModel>>(Role);
                return View(MappedRoles);
            }
        }
        #endregion
        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var MappedRole = _mapper.Map<IdentityRole>(model);
                await _roleManager.CreateAsync(MappedRole);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        #endregion
    }
}
