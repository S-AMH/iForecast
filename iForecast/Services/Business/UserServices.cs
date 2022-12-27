using iForecast.Entities;
using iForecast.Services.Data;
using Microsoft.EntityFrameworkCore;

namespace iForecast.Services.Business
{
    public class UserServices
    {
        private readonly DataContext dataContext;
        public UserServices(DataContext dataContext)
        {
            this.dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }
        public async Task Create(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            user.SetPassword(user.Password);
            await dataContext.Users.AddAsync(user);
            await dataContext.SaveChangesAsync();
        }
        public async Task Update(User _user)
        {
            if (_user == null)
                throw new ArgumentNullException(nameof(_user));
            User? user = await dataContext.Users.FindAsync(_user.Id);
            if (user == null)
                throw new KeyNotFoundException(nameof(_user.Id));
            user.Email = _user.Email;
            user.Name = _user.Name;
            user.SetPassword(_user.Password);
            await dataContext.SaveChangesAsync();
        }
        public async Task Delete(User _user)
        {
            if (_user == null)
                throw new ArgumentNullException(nameof(_user));
            User? user = await dataContext.Users.FindAsync(_user.Id);
            if (user == null)
                throw new KeyNotFoundException(nameof(_user.Id));
            dataContext.Users.Remove(user);
            await dataContext.SaveChangesAsync();
        }
        public async Task<ICollection<User>> ListAllUsers()
        {
            return await dataContext.Users.ToListAsync();
        }
        public async Task<User> GetUser(string id)
        {
            User? user = await dataContext.Users.FindAsync(id);
            if (user == null)
                throw new KeyNotFoundException(nameof(id));
            return user;
        }
    }
}
