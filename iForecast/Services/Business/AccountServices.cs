using iForecast.Entities;
using iForecast.Services.Data;
using Microsoft.EntityFrameworkCore;

namespace iForecast.Services.Business
{
    public class AccountServices
    {
        private readonly DataContext dataContext;
        private readonly UserServices userServices;
        public AccountServices(DataContext _dataContext)
        {
            this.dataContext = _dataContext;
            userServices = new UserServices(_dataContext);
        }
        public async Task Create(Account account)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));
            account.Owner = await userServices.GetUser(account.OwnerId);
            await dataContext.Accounts.AddAsync(account);
            await dataContext.SaveChangesAsync();
        }
        public async Task Update(Account _account)
        {
            if (_account == null)
                throw new ArgumentNullException(nameof(_account));
            Account? account = await dataContext.Accounts.FindAsync(_account.Id);
            if (account == null)
                throw new KeyNotFoundException(nameof(_account.Id));
            account.Owner = await userServices.GetUser(_account.OwnerId);
            account.OwnerId = _account.OwnerId;
            account.Name = _account.Name;
            account.Total = _account.Total;
            await dataContext.SaveChangesAsync();
        }
        public async Task Delete(Account _account)
        {
            if (_account == null)
                throw new ArgumentNullException(nameof(_account));
            Account? account = await dataContext.Accounts.FindAsync(_account.Id);
            if (account == null)
                throw new KeyNotFoundException(nameof(account.Id));
            dataContext.Accounts.Remove(account);
            await dataContext.SaveChangesAsync();
        }
        public async Task<ICollection<Account>> ListAllAcconuts(string ownerId)
        {
            ICollection<Account> accounts = (await dataContext.Accounts.ToListAsync())
                .Where(account => account.OwnerId == ownerId).ToList();
            User owner = await userServices.GetUser(ownerId);
            foreach (Account account in accounts)
                account.Owner = owner;
            return accounts;
        }
        public async Task<Account> GetAccount(string id)
        {
            Account? account = await dataContext.Accounts.FindAsync(id);
            if (account == null)
                throw new KeyNotFoundException(nameof(id));
            return account;
        }
    }
}
