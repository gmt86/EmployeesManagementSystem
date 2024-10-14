using EmployeesManagement.Data;
using EmployeesManagement.Models;
using EmployeesManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EmployeesManagement.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context,UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager)
        {
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userManager = userManager;            
            _context = context;
        }
        public async Task<ActionResult> Index()
        {
            //var users = await _context.Users.Include(x =>x.Roles).ToListAsync();
            var users = await _context.Users.ToListAsync();
            return View(users);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            ViewData["RoleId"] =  new  SelectList(_context.Roles ,"Id","Name");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(UsersViewModels viewModels)
        {
            ApplicationUser user = new ApplicationUser();
            user.UserName = viewModels.UserName;
            user.FirstName = viewModels.FirstName;
            user.LastName = viewModels.LastName;
            user.NationalId = viewModels.NationalId;
            user.RoleId = viewModels.RoleId;

            user.NormalizedUserName = viewModels.UserName;
            user.Email = viewModels.Email;
            user.PhoneNumberConfirmed = true;
            user.PhoneNumber = viewModels.PhoneNumber;
            user.PhoneNumberConfirmed = true;

            user.CreatedOn = DateTime.Now;
            user.CreatedById = "0";


           var result= await _userManager.CreateAsync(user, viewModels.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(viewModels);
            }

            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", viewModels.RoleId);

        }

        //[HttpGet]
        //public async Task<ActionResult> Edit(string id)
        //{
        //    //var user = new UsersViewModels();
        //    //var result = await _userManager.FindByIdAsync(id);
        //    //user.Id = result.Id;
        //    //user.PhoneNumber = result.PhoneNumber;
        //    //user.Email = result.Email;
        //    //user.UserName = result.UserName;
        //    //return View(user);
        //}

        //[HttpPost]
        //public async Task<ActionResult> Edit(string id, UsersViewModels viewModels)
        //{
        //    //var user = await _userManager.FindByIdAsync(id);
        //    //user.UserName = viewModels.UserName;
        //    //user.NormalizedUserName = viewModels.UserName;
        //    //user.Email = viewModels.Email;
        //    //user.PhoneNumberConfirmed = true;
        //    //user.PhoneNumber = viewModels.PhoneNumber;
        //    //user.PhoneNumberConfirmed = true;

        //    //var result = await _userManager.UpdateAsync(user);

        //    //if (result.Succeeded)
        //    //{
        //    //    return RedirectToAction("Index");
        //    //}
        //    //else
        //    //{
        //    //    return View(viewModels);
        //    //}

        //}
    }
}
