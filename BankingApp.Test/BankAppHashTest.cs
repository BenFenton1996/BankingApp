using NUnit.Framework;

namespace BankingApp.Test
{
    class BankAppHashTest
    {
        [Test]
        public void TestHashText()
        {
            var plainText = "TestString";
            byte[] salt = Utilities.BankingAppHash.GenerateSalt();
            byte[] hashedText = Utilities.BankingAppHash.HashText(plainText, salt);
            Assert.AreNotEqual(plainText, hashedText);
            Assert.AreEqual(32, hashedText.Length);
        }
    }
}
