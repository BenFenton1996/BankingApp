using NUnit.Framework;

namespace BankingApp.Test
{
    [TestFixture]
    class BankAppEncryptionTest
    {
        [Test]
        public void TestEncryption()
        {
            string plainText = "TestString";
            string cipherText = Utilities.BankingAppEncryption.EncryptString(plainText);
            Assert.AreNotEqual(plainText, cipherText);
            Assert.AreEqual(44, cipherText.Length);
        }
    }
}
