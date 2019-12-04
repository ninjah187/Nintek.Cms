using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nintek.Cms.Users.Services
{
    public class HashGenerator
    {
        readonly SaltGenerator _saltGenerator;

        public HashGenerator(SaltGenerator saltGenerator)
        {
            _saltGenerator = saltGenerator;
        }

        public (string hash, string salt) GetHash(string input)
        {
            // generate 128-bit salt
            var salt = _saltGenerator.GetSalt(128 / 8);
            return GetHash(input, salt);
        }

        public (string hash, string salt) GetHash(string input, string salt)
        {
            var saltBytes = Convert.FromBase64String(salt);
            return GetHash(input, saltBytes);
        }

        static (string hash, string salt) GetHash(string input, byte[] salt)
        {
            var hash = KeyDerivation.Pbkdf2(
                input,
                salt,
                KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8);
            var base64 = (hash: Convert.ToBase64String(hash), salt: Convert.ToBase64String(salt));
            return base64;
        }
    }
}
