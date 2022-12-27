using iForecast.Entities;
using iForecast.Services.Business;
using iForecast.Services.Data;
using Microsoft.AspNetCore.Mvc;

namespace iForecast.Controllers
{
    public class UserController : Controller
    {
        private readonly UserServices userServices;
        public UserController(DataContext dataContext)
        {
            this.userServices = new UserServices(dataContext);
        }
        public async Task<IActionResult> Index()
        {
            ICollection<User> users = await userServices.ListAllUsers();
            return View(users);
        }
        public IActionResult Register()
        {
            return View("Register");
        }
        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            try
            {
                await userServices.Create(user);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Error", "Home");
            }
        }
    }
}
