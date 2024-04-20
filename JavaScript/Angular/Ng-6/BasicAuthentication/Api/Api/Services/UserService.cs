using Api.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
        Task<IEnumerable<User>> GetAll();
    }

    public class UserService : IUserService
    {
        private List<User> _users = new List<User>
        {
            new User() {Id = 1, FirstName="Mohd", LastName="Arshad", Username="Delhi", Password="Del"},
            new User() {Id = 2, FirstName="Mohd", LastName="Asjad", Username="Dehradun", Password="Ddn"}
        };

        public async Task<User> Authenticate(string username, string password)
        {
            var user = await Task.Run(() => _users.SingleOrDefault(u => u.Username == username && u.Password == password));
            if (user == null) return null;
            user.Password = null; //Authentication success, return user without password.
            return user;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await Task.Run(() => _users.Select(x => {
                x.Password = null;
                return x;
            }));
        }
    }
}
