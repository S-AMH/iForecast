using iForecast.Entities;
using iForecast.Services.Business;
using iForecast.Services.Data;
using Microsoft.AspNetCore.Mvc;

namespace iForecast.Controllers
{
    public class SubAccountController : Controller
    {
        private SubAccountServices subAccountServices;
        private AccountServices accountServices;
        public SubAccountController(DataContext dataContext)
        {
            subAccountServices = new SubAccountServices(dataContext);
            accountServices = new AccountServices(dataContext);
        }
        public async Task<IActionResult> Index(Account account)
        {
            ICollection<SubAccount> subAccounts = await subAccountServices.GetAllSubAccounts(account.Id);
            return View(subAccounts);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(SubAccount subAccount)
        {
            await subAccountServices.Create(subAccount);
            return RedirectToAction("Index", new { Id = subAccount.ParentId });
        }
    }
}
