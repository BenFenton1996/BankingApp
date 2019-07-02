using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace SocialMediaProject.Utilities
{
    public static class SMPHash
    {
        /// <summary>
        /// Hashes a string using HMACSHA1 with 10,000 iterations and returns the resulting 256-bit byte array
        /// </summary>
        /// <param name="plainText">The string to hash</param>
        /// <param name="salt">The salt to use when hashing</param>
        /// <returns>The result of hashing the plainText string</returns>
        public static byte[] HashText(string plainText, byte[] salt)
        {
            return KeyDerivation.Pbkdf2(
                password: plainText,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 32);
        }

        /// <summary>
        /// Generates a 128-bit salt using a cryptographically secure PRNG (Psuedorandom Number Generator)
        /// </summary>
        /// <returns>A byte array containing a salt for use when hashing</returns>
        public static byte[] GenerateSalt()
        {
            byte[] salt = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
                return salt;
            }
        }
    }
}
