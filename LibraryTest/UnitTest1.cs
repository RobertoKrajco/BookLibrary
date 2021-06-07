using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library;

namespace LibraryTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestLogin()
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            bool logged = loginForm.Login("user", "user123");
            Assert.AreEqual(true, logged);
        }

        [TestMethod]
        public void TestRemove()
        {
            LibraryForm libraryForm = new LibraryForm();
            libraryForm.rem
            libraryForm.Show();
        }
    }
}
