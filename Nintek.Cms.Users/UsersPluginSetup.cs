using Microsoft.Extensions.Configuration;
using Nintek.Cms.Users.Models;
using Nintek.Cms.Users.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nintek.Cms.Users
{
    public class UsersPluginSetup : PluginSetup
    {
        protected override async Task Initialize(IServiceProvider serviceProvider)
        {
            var bank = serviceProvider.GetService<Bank>();
            var hashGenerator = serviceProvider.GetService<HashGenerator>();
            var configuration = serviceProvider.GetService<IConfiguration>();

            var name = configuration.GetValue<string>("Nintek:Cms:Users:Admin:Name") ?? "admin";
            var password = configuration.GetValue<string>("Nintek:Cms:Users:Admin:Password") ?? "admin";

            var (hash, salt) = hashGenerator.GetHash(password);

            var user = new User(name, hash, salt, new[] { Role.Admin, Role.User });

            bank.Save(user);
        }
    }
}
