using NUnit.Framework;

namespace BankingApp.Test
{
    [TestFixture]
    internal class BankAppEncryptionTest
    {
        [Test]
        public void TestEncryption()
        {
            var plainText = "TestString";
            var cipherText = Utilities.BankingAppEncryption.EncryptString(plainText);
            Assert.AreNotEqual(plainText, cipherText);
            Assert.AreEqual(44, cipherText.Length);
        }
    }
}
