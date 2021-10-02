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

        // tests added by Kęstutis Keršis:

        [TestMethod]
        public void Email_Has_Spaces_Not_Valid()
        {
            var isValid = EmailValidator.Check("kker sis@gmail.com");
            Assert.AreEqual(false, isValid, "Has spaces");
        }

        [TestMethod]
        public void Email_Has_Multiple_At_Signs_Not_Valid()
        {
            var isValid = EmailValidator.Check("kkersis@kkersis@kkersis@gmail.com");
            Assert.AreEqual(false, isValid, "Multiple @ signs");
        }

        [TestMethod]
        public void Email_No_Domain_Not_Valid()
        {
            var isValid = EmailValidator.Check("kkersis@.lt");
            Assert.AreEqual(false, isValid, "No domain");
        }

        [TestMethod]
        public void Email_No_TLD_Not_Valid()
        {
            var isValid = EmailValidator.Check("kkersis@pimpim.");
            Assert.AreEqual(false, isValid, "No TLD");
        }
    }
}
