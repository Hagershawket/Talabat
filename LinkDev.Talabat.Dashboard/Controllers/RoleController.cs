using LinkDev.Talabat.Dashboard.ViewModels.Role;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.Talabat.Dashboard.Controllers
{
    public class RoleController(RoleManager<IdentityRole> _roleManager) : Controller
    {
        #region Index

        [HttpGet] // GET: /Role/Index
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();

            return View(roles);
        }

        #endregion

        #region Create

        [HttpPost] // POST: /Role/Create
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleFormViewModel model)
        {
            if (!ModelState.IsValid)            
                return View("Index", model);

            var roleExist = await _roleManager.RoleExistsAsync(model.Name);
            if (!roleExist)
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = model.Name.Trim() });
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("Name", "This Role is already Exist");
                return View("Index", await _roleManager.Roles.ToListAsync());
            }

        }

        #endregion


    }
}
