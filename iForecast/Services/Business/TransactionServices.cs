using iForecast.Entities;
using iForecast.Services.Data;

namespace iForecast.Services.Business
{
    public class TransactionServices
    {
        private readonly DataContext dataContext;
        private readonly SubAccountServices subAccountServices;
        public TransactionServices(DataContext dataContext)
        {
            this.dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            this.subAccountServices = new SubAccountServices(dataContext);
        }
        public async Task Create(Transaction transaction)
        {
            if (transaction == null)
                throw new ArgumentNullException(nameof(transaction));
            SubAccount subAccount = await subAccountServices.GetSubAccount(transaction.SubAccountId);
            transaction.SubAccount = subAccount;
            await dataContext.Transactions.AddAsync(transaction);
            await dataContext.SaveChangesAsync();
            subAccount.Total += transaction.Amount;
            await subAccountServices.Update(subAccount);
        }
    }
}
