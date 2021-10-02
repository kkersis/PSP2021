using Microsoft.VisualStudio.TestTools.UnitTesting;
using PSP;

namespace ValidationTests
{
    [TestClass]
    public class PhoneValidatorTests
    {
        [TestMethod]
        public void PhoneNumber_DoesNotHaveOtherCharacters_ShouldNotBeValid()
        {
            var isValid = PhoneValidator.Check("+37063363686abc");
            Assert.AreEqual(false, isValid, "Phone number should not have other characters");
        }

        [TestMethod]
        public void PhoneNumber_DoesNotHaveOtherCharaters_ShouldBeValid()
        {
            var isValid = PhoneValidator.Check("+37063363686");
            Assert.AreEqual(true, isValid);
        }

        [TestMethod]
        public void PhoneNumber_ChangePrefix_ShouldNotChange()
        {
            var changedNumber = PhoneValidator.ChangePrefix("763363686");
            Assert.AreEqual("763363686", changedNumber);
        }

        [TestMethod]
        public void PhoneNumber_ChangePrefix_ShouldChange()
        {
            var changedNumber = PhoneValidator.ChangePrefix("863363686");
            Assert.AreEqual("+37063363686", changedNumber);
        }

        [TestMethod]
        public void PhoneNumber_AddValidationRule_ShouldAdd()
        {
            PhoneValidator.AddValidationRule("United Kingdom", 11, "0", "+44");
            var changedNumber = PhoneValidator.ChangePrefix("01165604691");
            Assert.AreEqual("+441165604691", changedNumber);
        }
    }
}
