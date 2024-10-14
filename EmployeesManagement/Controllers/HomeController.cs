using EmployeesManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EmployeesManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {          
            //return !User.Identity.IsAuthenticated ? this.Redirect("~/Identity/Account/Login") : this.Redirect("index");
            //return User.Identity.IsAuthenticated ? this.Redirect("index")  : this.Redirect("~/Identity/Account/Login");
            return !User.Identity.IsAuthenticated ? this.Redirect("~/Identity/Account/Login"): View()  ;
            //return Redirect("~/Identity/Account/Login");
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
