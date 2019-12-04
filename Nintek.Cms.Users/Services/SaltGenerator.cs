using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Nintek.Cms.Users.Services
{
    public class SaltGenerator
    {
        public byte[] GetSalt(int length)
        {
            var salt = new byte[length];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }
    }
}
