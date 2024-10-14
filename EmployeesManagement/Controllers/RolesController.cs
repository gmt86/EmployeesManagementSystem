using EmployeesManagement.Data;
using EmployeesManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EmployeesManagement.Controllers
{
    public class RolesController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public RolesController(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager)
        {
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
        }
        public async Task<ActionResult> Index()
        {
            var roles = await _context.Roles.ToListAsync();
            return View(roles);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(RolesViewModels viewModels)
        {
            IdentityRole role = new IdentityRole();
            role.Name = viewModels.RoleName;           

            var result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(viewModels);
            }

        }


        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            var role = new RolesViewModels();
            var result = await _roleManager.FindByIdAsync(id);
            role.Id = result.Id;
            role.RoleName = result.Name;
            return View(role);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(string id, RolesViewModels viewModels)
        {
            var existe = await _roleManager.RoleExistsAsync(viewModels.RoleName);

            if (!existe)
            {
                var Role = await _roleManager.FindByIdAsync(id);
                Role.Name = viewModels.RoleName;

                var result = await _roleManager.UpdateAsync(Role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(viewModels);
                }
            }                        

            return View(viewModels);
        }
    }
}
