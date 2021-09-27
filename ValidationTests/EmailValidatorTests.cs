using Microsoft.VisualStudio.TestTools.UnitTesting;
using PSP;

namespace ValidationTests
{
    [TestClass]
    public class EmailValidatorTests
    {
        [TestMethod]
        public void Email_AtCharacter_ShouldNotBeValid()
        {
            var isValid = EmailValidator.Check("andrius.vasiljevas.vu.lt");
            Assert.AreEqual(false, isValid, "Email should have @ character");
        }

        [TestMethod]
        public void Email_AtCharacter_ShouldBeValid()
        {
            var isValid = EmailValidator.Check("andrius.vasiljevas@vu.lt");
            Assert.AreEqual(true, isValid);
        }

        [TestMethod]
        public void Email_OtherCharacters_ShoudNotBeValid()
        {
            var isValid = EmailValidator.Check("Andrius43%~-@vU2.lt");
            Assert.AreEqual(false, isValid, "Email has special characters that are not allowed"); ;
        }

        [TestMethod]
        public void Email_OtherCharacters_ShouldBeValid()
        {
            var isValid = EmailValidator.Check("andrius.vasiljevas@mif.vu.lt");
            Assert.AreEqual(true, isValid);
        }

        [TestMethod]
        public void Email_Domain_ShouldNotBeValid()
        {
            var isValid = EmailValidator.Check("andrius.vasiljevas@lalalalala.vuvu.lt.co.uk");
            Assert.AreEqual(false, isValid, "Email should have a proper domain and TLD");
        }

        [TestMethod]
        public void Email_Domain_ShouldBeValid()
        {
            var isValid = EmailValidator.Check("andrius.vasiljevas@mif.stud.vu.lt");
            Assert.AreEqual(true, isValid);
        }
    }
}
