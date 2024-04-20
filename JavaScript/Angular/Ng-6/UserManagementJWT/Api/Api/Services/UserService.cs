using Api.Entities;
using Api.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        User GetById(int id);
        User Create(User user, string password);
        void Update(User userParam, string password = null);
        void Delete(int id);
    }
    public class UserService : IUserService
    {
        private DataContext _dataContext;

        public UserService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public User Authenticate(string username, string password)
        {
            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
                return null;

            var user = _dataContext.Users.SingleOrDefault(x => x.Username == username);
            if (user == null) return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        public User Create(User user, string password)
        {
            if (String.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required.");

            if (_dataContext.Users.Any(x => x.Username == user.Username))
                throw new AppException($"Username {user.Username} is already taken.");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _dataContext.Users.Add(user);
            _dataContext.SaveChanges();

            return user;
        }

        public void Delete(int id)
        {
            var user = _dataContext.Users.Find(id);
            if(user != null)
            {
                _dataContext.Users.Remove(user);
                _dataContext.SaveChanges();
            }
        }

        public IEnumerable<User> GetAll()
        {
            return _dataContext.Users;
        }

        public User GetById(int id)
        {
            return _dataContext.Users.Find(id);
        }

        public void Update(User userParam, string password = null)
        {
            var user = _dataContext.Users.Find(userParam.Id);

            if (user == null)
                throw new AppException("User not found.");

            if(userParam.Username != user.Username)
            {
                if (_dataContext.Users.Any(x => x.Username == userParam.Username))
                    throw new AppException($"Username {userParam.Username} already exists.");
            }

            user.FirstName = userParam.FirstName;
            user.LastName = userParam.LastName;
            user.Username = user.Username;

            if (string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            _dataContext.Users.Update(user);
            _dataContext.SaveChanges();
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64)
                throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128)
                throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
