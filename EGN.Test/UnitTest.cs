using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EGN.Test
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        [DataRow("hskkdhsd")]
        [DataRow("30djd2-d98s")]
        [DataRow("333-244224")]
        [DataRow("333-24422 ")]
        public void When_egn_is_not_only_numbers_should_throw_error(string egn)
        {
            Assert.ThrowsException<Exception>(() => Program.CheckIfContainsOnlyTenNumbers(egn));
        }

        [TestMethod]
        [DataRow("256598456")]
        [DataRow("698656144")]
        [DataRow("000222556")]
        [DataRow("987456321")]
        public void When_entered_only_numbers_should_pass_test(string egn)
        {
            try
            {
                Program.CheckIfContainsOnlyTenNumbers(egn);
            }
            catch (Exception ex)
            {
                StringAssert.Contains(ex.Message, "You entered bad characters! Please enter only numbers! ");
                Assert.Fail("Error was thrown");
            }
        }

        [TestMethod]
        [DataRow(0, 0)]
        [DataRow(2, 0)]
        [DataRow(3, 4)]
        [DataRow(7, 0)]
        public void When_wrong_months_are_entered(int numb1, int numb2)
        {
            Assert.ThrowsException<Exception>(() => Program.CheckMonths(numb1, numb2));
        }

        [DataRow(0, 1)]
        [DataRow(1, 2)]
        public void When_correct_months_are_entered(int numb1, int numb2)
        {
          var result = Program.CheckMonths(numb1, numb2);

          Assert.AreEqual(numb1*10+numb2, result);
        }

        [TestMethod]
        [DataRow(2, 1)]       
        public void When_correct_months_are_entered_old_year(int numb1, int numb2)
        {
            var result = Program.CheckMonths(numb1, numb2);

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        [DataRow(5, 0)]
        public void When_correct_months_are_entered_new_year(int numb1, int numb2)
        {
            var result = Program.CheckMonths(numb1, numb2);

            Assert.AreEqual(10, result);
        }

        [TestMethod]
        [DataRow(3, 1, 2, 1992)]
        [DataRow(0, 0, 2, 2000)]
        [DataRow(3, 1, 2, 1992)]
        [DataRow(9, 1, 2, 1802)]
        public void When_wrong_days_are_entered(int firstDayDigit, int secontDayDigit, int birthMonth, int birthYear)
        {
            Assert.ThrowsException<Exception>(() => Program.CheckBirthDays(firstDayDigit, secontDayDigit, birthMonth, birthYear));
        }

        [TestMethod]
        [DataRow(2, 8, 2, 1992)]
        [DataRow(1, 0, 4, 2000)]
        [DataRow(3, 1, 3, 1994)]
        [DataRow(0, 1, 12, 1802)]
        public void When_correct_days_are_entered(int firstDayDigit, int secontDayDigit, int birthMonth, int birthYear)
        {
            try
            {
                Program.CheckBirthDays(firstDayDigit, secontDayDigit, birthMonth, birthYear);
            }
            catch (Exception ex)
            {
                StringAssert.Contains(ex.Message, "The date is NOK");
                Assert.Fail("Error was thrown");
            }
        }

        [TestMethod]
        [DataRow(0, 0)]
        public void Check_when_zeroes_are_enterd_for_baby_number(int numb1, int numb2)
        {
            Assert.ThrowsException<Exception>(() => Program.CheckBabyNumber(numb1, numb2));
        }

        [TestMethod]
        [DataRow(1, 0)]
        [DataRow(9, 9)]
        public void Check_when_baby_number_isentered_ok(int numb1, int numb2)
        {
            try
            {
                Program.CheckBabyNumber(numb1, numb2);
            }
            catch (Exception ex)
            {
                StringAssert.Contains(ex.Message, "The date is NOK");
                Assert.Fail("You entered wrong baby number!");
            }
        }

        // for last method - CheckLastDigit() - I will not write an tests because for checking if the method works i need valid egnss
    }
}
