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


    }
}
