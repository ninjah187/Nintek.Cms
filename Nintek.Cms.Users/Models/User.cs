using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nintek.Cms.Users.Models
{
    public class User : Model
    {
        public User(string email, string password, string salt, string[] roles)
            : this(0, email, password, salt, roles)
        {
        }

        [JsonConstructor]
        public User(int id, string email, string password, string salt, string[] roles)
            : base(id)
        {
            Id = id;
            Email = email;
            Password = password;
            Salt = salt;
            Roles = roles;
        }

        public string Email { get; }
        public string Password { get; }
        public string Salt { get; }
        public string[] Roles { get; }

        public bool HasRole(string role) => Roles.Any(x => string.Equals(x, role, StringComparison.InvariantCultureIgnoreCase));

        public bool IsAdmin() => HasRole(Role.Admin);
        public bool IsUser() => HasRole(Role.Admin) || HasRole(Role.User);
    }
}
