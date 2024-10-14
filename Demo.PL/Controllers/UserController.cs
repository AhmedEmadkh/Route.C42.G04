using AutoMapper;
using Demo.DAL.Entities.Identity;
using Demo.DAL.Presistance.Data;
using Demo.PL.ViewModels.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.PL.Controllers
{
    public class UserController : Controller
    {
        #region Services
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<ApplicationUser> userManager,IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        } 
        #endregion
        #region Index
        public async Task<IActionResult> Index(string SearchedValue)
        {
            if (string.IsNullOrEmpty(SearchedValue))
            {
                var Users = await _userManager.Users.Select(
                     U => new UserViewModel()
                     {
                         Id = U.Id,
                         Fname = U.FName,
                         LName = U.LName,
                         Email = U.Email ?? string.Empty,
                         PhoneNumber = U.PhoneNumber ?? string.Empty,
                         Roles = _userManager.GetRolesAsync(U).Result
                     }).ToListAsync();
                return View(Users);
            }
            else
            {
                var User = await _userManager.FindByEmailAsync(SearchedValue);

                if (User is null)
                {
                    return NotFound();
                }

                var MappedUser = new UserViewModel
                {
                    Id = User.Id,
                    Fname = User.FName,
                    LName = User.LName,
                    Email = User?.Email ?? string.Empty,
                    PhoneNumber = User?.PhoneNumber ?? string.Empty,
                    Roles = _userManager.GetRolesAsync(User!).Result
                };
                return View(new List<UserViewModel> { MappedUser });
            }
        }
        #endregion
        #region Details
        [HttpGet] // Get: User/Details
        public async Task<IActionResult> Details(string Id,string ViewName = "Details")
        {
            if (Id == null)
                return BadRequest();
            var User = await _userManager.FindByIdAsync(Id);

            if(User is null)
                return NotFound();
            var MappedUser = _mapper.Map<ApplicationUser, UserViewModel>(User);
            return View(ViewName,MappedUser);
        }
        #endregion
        #region Edit
        [HttpGet] // Get: User/Edit
        public async Task<IActionResult> Edit(string Id)
        {
            return await Details(Id,"Edit");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel model, [FromRoute] string Id)
        {
            if (Id != model.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    var existingUser = await _userManager.FindByIdAsync(Id);
                    if (existingUser is null)
                        return NotFound();

                    existingUser.FName = model.Fname;
                    existingUser.LName = model.LName;
                    existingUser.PhoneNumber = model.PhoneNumber;

                    var result = await _userManager.UpdateAsync(existingUser);
                    if (result.Succeeded)
                        return RedirectToAction(nameof(Index));

                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            else
            {
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        Console.WriteLine($"Error in {state.Key}: {error.ErrorMessage}");
                    }
                }
            }
            return View(model);
        }


        #endregion
        #region Delete
        public async Task<IActionResult> Delete(string Id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(Id);
                await _userManager.DeleteAsync(user);
                return RedirectToAction(nameof(Index));
            }catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty , ex.Message);
                return RedirectToAction("Error",ex.Message);
            }
        }
        #endregion
    }
}
