using Nintek.Cms.Users.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nintek.Cms.Users.Services
{
    public class UserService
    {
        readonly Bank _bank;
        readonly HashGenerator _hashGenerator;

        public UserService(Bank bank, HashGenerator hashGenerator)
        {
            _bank = bank;
            _hashGenerator = hashGenerator;
        }

        public Task<User> RegisterAdmin(string name, string password) =>
            Register(name, password, Role.Admin, Role.User);

        public Task<User> RegisterUser(string name, string password) =>
            Register(name, password, Role.User);

        public async Task<Session> SignIn(string name, string password)
        {
            var user = GetUser(name);
            if (user == null)
            {
                throw new CmsException($"User {name} does not exist.");
            }
            if (!HashMatches(password, user.Password, user.Salt))
            {
                throw new CmsException($"Invalid password for user {name}.");
            }
            var newKey = Guid.NewGuid().ToString().Replace("-", "");
            var session = new Session(user.Id, newKey, DateTime.UtcNow);
            await _bank.Save(session).Commit();
            return session;
        }

        public async Task SignOut(int sessionId)
        {
            _bank.Session.Delete<Session>(sessionId);
            await _bank.Commit();
        }

        async Task<User> Register(string name, string password, params string[] roles)
        {
            var existing = GetUser(name);
            if (existing != null)
            {
                throw new CmsException($"User {name} already exists.");
            }
            var (hash, salt) = _hashGenerator.GetHash(password);
            var user = new User(name, hash, salt, roles);
            await _bank.Save(user).Commit();
            return user;
        }

        User GetUser(string name)
        {
            return _bank
                .Session
                .Query<User>()
                .FirstOrDefault(x => string.Equals(x.Email, name, StringComparison.InvariantCultureIgnoreCase));
        }

        bool HashMatches(string plain, string password, string salt)
        {
            var (hash, _) = _hashGenerator.GetHash(plain, salt);
            return hash == password;
        }
    }
}
