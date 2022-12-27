using iForecast.Entities;
using iForecast.Services.Data;
using Microsoft.EntityFrameworkCore;

namespace iForecast.Services.Business
{
    public class SubAccountServices
    {
        private readonly DataContext dataContext;
        private readonly AccountServices accountServices;
        public SubAccountServices(DataContext dataContext)
        {
            this.dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            accountServices = new AccountServices(dataContext);
        }
        public async Task Create(SubAccount subAccount)
        {
            if (subAccount == null)
                throw new ArgumentNullException(nameof(subAccount));
            Account account = await accountServices.GetAccount(subAccount.ParentId);
            subAccount.Parent = account;
            await dataContext.SubAccounts.AddAsync(subAccount);
            await dataContext.SaveChangesAsync();
        }
        public async Task Update(SubAccount _subAccount)
        {
            decimal t = 0;
            if (_subAccount == null)
                throw new ArgumentNullException(nameof(_subAccount));
            SubAccount? subAccount = await dataContext.SubAccounts.FindAsync(_subAccount.Id);
            if (subAccount == null)
                throw new KeyNotFoundException(nameof(_subAccount));
            Account account = await accountServices.GetAccount(_subAccount.ParentId);
            t = _subAccount.Total - subAccount.Total;
            subAccount.ParentId = _subAccount.ParentId;
            subAccount.Name = _subAccount.Name;
            subAccount.Total = _subAccount.Total;
            subAccount.Parent = account;
            await dataContext.SubAccounts.AddAsync(subAccount);
            await dataContext.SaveChangesAsync();
            account.Total += t;
            await accountServices.Update(account);
        }
        public async Task Delete(SubAccount _subAccount)
        {
            if (_subAccount == null)
                throw new ArgumentNullException(nameof(_subAccount));
            SubAccount? subAccount = await dataContext.SubAccounts.FindAsync(_subAccount.Id);
            if (subAccount == null)
                throw new KeyNotFoundException(nameof(_subAccount.Id));
            dataContext.SubAccounts.Remove(subAccount);
            await dataContext.SaveChangesAsync();
        }
        public async Task<SubAccount> GetSubAccount(string id)
        {
            SubAccount? subAccount = await this.dataContext.SubAccounts.FindAsync(id);
            if (subAccount == null)
                throw new KeyNotFoundException(nameof(id));
            return subAccount;
        }
        public async Task<ICollection<SubAccount>> GetAllSubAccounts(string accountId)
        {
            ICollection<SubAccount> subAccounts = (await dataContext.SubAccounts.ToListAsync())
                .Where(subAccount => subAccount.ParentId == accountId).ToList();
            Account parent = await accountServices.GetAccount(accountId);
            foreach (SubAccount subAccount in subAccounts)
                subAccount.Parent = parent;
            return subAccounts;
        }
    }
}
