using Microsoft.VisualStudio.TestTools.UnitTesting;
using PSP;

namespace ValidationTests
{
    [TestClass]
    public class PasswordCheckerTests
    {
        [TestMethod]
        public void Password_TooShort_ShouldNotBeValid()
        {
            var isValid = PasswordChecker.Check("a");
            Assert.AreEqual(false, isValid, "Password is too short");
        }

        [TestMethod]
        public void Password_TooShort_ShouldBeValid()
        {
            var isValid = PasswordChecker.Check("Abcde#gh123345678900");
            Assert.AreEqual(true, isValid);
        }

        [TestMethod]
        public void Password_HasUpperCase_ShouldNotBeValid()
        {
            var isValid = PasswordChecker.Check("abcde#gh123");
            Assert.AreEqual(false, isValid, "Password should have an uppercase character");
        }

        [TestMethod]
        public void Password_HasUpperCase_ShouldBeValid()
        {
            var isValid = PasswordChecker.Check("Abcde#gh123");
            Assert.AreEqual(true, isValid);
        }

        [TestMethod]
        public void Password_HasSpecialSymbol_ShouldNotBeValid()
        {
            var isValid = PasswordChecker.Check("Abcdefgh123");
            Assert.AreEqual(false, isValid, "Password should have a special character");
        }

        [TestMethod]
        public void Password_HasSpecialSymbol_ShouldBeValid()
        {
            var isValid = PasswordChecker.Check("Abc%^7de#gh123");
            Assert.AreEqual(true, isValid);
        }
    }
}
