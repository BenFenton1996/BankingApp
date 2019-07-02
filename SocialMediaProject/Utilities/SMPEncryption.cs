using System;
using System.IO;
using System.Security.Cryptography;

namespace SocialMediaProject.Utilities
{
    /// <summary>
    /// Contains easy to use encryption and decryption methods, both of which 
    /// accept a string and return the result of encrypting or decrypting it as a string
    /// </summary>
    public class SMPEncryption
    {
        //The Symmetric-key used for encrypting and decrypting (MUST BE 128 BITS/16 BYTES, 192 BITS/24 BYTES OR 256 BITS/32 BYTES)
        private static readonly byte[] Key = { 0x66, 0x32, 0x33, 0x64, 0x6a, 0x33, 0x32, 0x66, 0x32, 0x70, 0x33, 0x6d, 0x6b, 0x75, 0x32, 0x33 };

        /// <summary>
        /// Encrypts a value using AES (Advanced Encryption Standard) with PKCS7 padding with the initialization vector as the first 16 bytes 
        /// and returns the encrypted value as a Base64 string
        /// </summary>
        /// <param name="plainText">The string to be encrypted</param>
        /// <returns>The encrypted value as a Base64 string</returns>
        public static string EncryptString(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentNullException("Value to be encrypted is null or empty");

            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.Padding = PaddingMode.PKCS7;
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (MemoryStream memStream = new MemoryStream())
                {
                    //Prepend the initialization vector to the beginning of the encrypted value
                    memStream.Write(aes.IV, 0, aes.IV.Length);
                    using (CryptoStream cryptoStream = new CryptoStream(memStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        var buffer = memStream.ToArray();
                        return Convert.ToBase64String(buffer, 0, buffer.Length);
                    }
                }
            }
        }

        /// <summary>
        /// Pulls the initialization vector from the first 16 bytes of the ciphered value and uses it with the key
        /// to decrypt the cipherTextString and returns the result as a string
        /// </summary>
        /// <param name="cipherTextString">The string to be decrypted</param>
        /// <returns>The result of decrypting cipherTextString as a string</returns>
        public static string DecryptString(string cipherTextString)
        {
            if (string.IsNullOrEmpty(cipherTextString))
                throw new ArgumentNullException("Value to be decrypted is null or empty");

            byte[] cipherText = Convert.FromBase64String(cipherTextString);
            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.Padding = PaddingMode.PKCS7;

                using (MemoryStream memStream = new MemoryStream(cipherText))
                {
                    //Pull the initalization vector from the cipher text
                    byte[] iv = new byte[16];
                    memStream.Read(iv, 0, 16);
                    aes.IV = iv;
                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                    using (CryptoStream cryptoStream = new CryptoStream(memStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
