using System;
using System.Security.Cryptography;
using System.Text;

namespace SocialMediaProject.Utilities
{
    public static class SMPHash
    {
        /// <summary>
        /// Hashes a string and returns the result as a string
        /// </summary>
        /// <param name="plainText">The string to hash</param>
        /// <returns>The result of hashing the plainText string</returns>
        public static string HashString(string plainText)
        {
            StringBuilder stringBuilder = new StringBuilder();

            using (var algorithm = SHA256.Create())
            {
                Encoding encoding = Encoding.UTF8;
                Byte[] result = algorithm.ComputeHash(encoding.GetBytes(plainText));

                foreach (Byte b in result)
                    stringBuilder.Append(b.ToString("x2"));
            }

            return stringBuilder.ToString();
        }
    }
}
