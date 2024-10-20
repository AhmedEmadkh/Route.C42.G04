using AutoMapper;
using Demo.PL.ViewModels.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
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
                if (Role is not null)
                {
                var MappedRoles = _mapper.Map<IEnumerable<RoleViewModel>>(new List<IdentityRole> { Role });
                    return View(MappedRoles);

                }
                else
                {
                    return View(new List<RoleViewModel>());
                }
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
        #region Details
        [HttpGet]
        public async Task<IActionResult> Details(string Id,string ViewName = "Details")
        {
            if (string.IsNullOrEmpty(Id))
                return BadRequest();
            var Role = await _roleManager.FindByIdAsync(Id);
            if(Role is null)
                return NotFound();
            var MappedRole = _mapper.Map<RoleViewModel>(Role);
            return View(ViewName, MappedRole);
        }
        #endregion
        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(string Id)
        {
            return await Details(Id, "Edit");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(RoleViewModel model,[FromRoute]string Id)
        {
            if (Id != model.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var existingRole = await _roleManager.FindByIdAsync(Id);
                    if(existingRole is null)
                        return NotFound();
                    existingRole.Name = model.RoleName;
                    await _roleManager.UpdateAsync(existingRole);
                    return RedirectToAction(nameof(Index));
                }catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(model);
        }
        #endregion
        #region Delete
        public async Task<IActionResult> Delete(string Id)
        {
            if (string.IsNullOrWhiteSpace(Id))
                return BadRequest();

            try
            {
                var Role = await _roleManager.FindByIdAsync(Id);
                await _roleManager.DeleteAsync(Role);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty,ex.Message);
                return RedirectToAction("Error", ex.Message);
            }
        }
        #endregion
    }
}
