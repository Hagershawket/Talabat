using LinkDev.Talabat.Dashboard.ViewModels.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.Talabat.Dashboard.Controllers
{
    [Authorize]
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

        [HttpGet] // GET: /Role/Create
        public IActionResult Create()
        {
            return View();
        }

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

        #region Edit

        [HttpGet]   // GET: /Role/Edit/id?
        public async Task<IActionResult> Edit(string? id)
        {
            if (id is null)
                return BadRequest();

            var role = await _roleManager.FindByIdAsync(id);
            if (role is null)
                return NotFound();

            return View(new RoleFormViewModel { Name = role.Name! });
        }

        [HttpPost] // POST
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, RoleFormViewModel model)
        {
            if (id is null)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(model);

            var roleExist = await _roleManager.FindByNameAsync(model.Name);
            if (roleExist is not null && roleExist.Id != id)            
            {
                ModelState.AddModelError("Name", "This Role is already Exist");
                return View(model);
            }
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null)
                return NotFound();

            role.Name = model.Name.Trim();
            await _roleManager.UpdateAsync(role);
            return RedirectToAction(nameof(Index));

        }

        #endregion

        #region Delete

        [HttpPost]  // POST
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string? id)
        {
            if (id is null)
                return BadRequest();

            var role = await _roleManager.FindByIdAsync(id);
            if (role is null)
            {
                ModelState.AddModelError("Name", "This Role is already exist");
                return View("Index", await _roleManager.Roles.ToListAsync());
            }

            await _roleManager.DeleteAsync(role);

            return RedirectToAction(nameof(Index));

        }

        #endregion
    }
}
