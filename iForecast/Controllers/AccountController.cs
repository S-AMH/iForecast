using iForecast.Entities;
using iForecast.Services.Business;
using iForecast.Services.Data;
using Microsoft.AspNetCore.Mvc;

namespace iForecast.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountServices accountServices;
        private readonly UserServices userServices;
        public AccountController(DataContext dataContext)
        {
            accountServices = new AccountServices(dataContext);
            userServices = new UserServices(dataContext);
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> MyAccount(User user)
        {
            try
            {
                ICollection<Account> accounts = await accountServices.ListAllAcconuts(user.Id);
                TempData["OwnerId"] = user.Id;
                return View(accounts);
            }
            catch { return View("Error"); }
        }
        [HttpPost]
        public async Task<IActionResult> Create(Account account)
        {
            if (account == null)
                return View();
            try
            {
                User? owner = (await userServices.ListAllUsers()).FirstOrDefault(x => x.Id == account.OwnerId);
                if (owner == null)
                    throw new KeyNotFoundException(nameof(owner));
                account.Owner = owner;
                await accountServices.Create(account);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Error", "Home");
            }
        }
    }
}
