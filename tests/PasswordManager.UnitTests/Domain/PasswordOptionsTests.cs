using PasswordManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.UnitTests.Domain
{
    public class PasswordOptionsTests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetUpperCaseOnly_LowerCaseIsFalse_IsValidState(bool upperCase)
        {
            PasswordOptions options = PasswordOptions.Defaults();

            options.SetUpperCase(upperCase);

            Assert.False(options.LowerCaseOnly && upperCase);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetLowerCaseOnly_UpperCaseIsFlase_IsValidState(bool lowerCase)
        {
            PasswordOptions options = PasswordOptions.Defaults();

            options.SetLowerCase(lowerCase);

            Assert.False(options.UpperCaseOnly && lowerCase);
        }

        [Fact]
        public void SetLowerCaseOnly_UpperCaseIsTrue_IfLowerCaseIsTrue_ThrowsInvalidOperationExceptionWithMessage()
        {
            PasswordOptions options = PasswordOptions.Defaults();
            options.SetUpperCase(true);
            string message = "Upper case and lower case can not be true at the same time!";

            var ex = Assert.Throws<InvalidOperationException>(() => options.SetLowerCase(true));

            Assert.Equal(message, ex.Message);
        }

        [Fact]
        public void SetUppearCaseOnly_LowerCaseIsTrue_IfUpperCaseIsTrue_ThrowsInvalidOperationExceptionWithMessage()
        {
            PasswordOptions options = PasswordOptions.Defaults();
            options.SetLowerCase(true);
            string message = "Upper case and lower case can not be true at the same time!";

            var ex = Assert.Throws<InvalidOperationException>(() => options.SetUpperCase(true));

            Assert.Equal(message, ex.Message);
        }

        [Fact]
        public void SetLength_FutureLengthIsLessThanPrevious_ThrowsInvalidOperationExceptionWithMessage()
        {
            PasswordOptions options = PasswordOptions.Defaults();
            int futureLength = options.Length - 1;
            string message = $"It is not possible to set the length ({futureLength}) less than the sum of the" +
                    $" number of digits ({options.CountOfNumbers}), the number of characters ({options.CountOfCharacters}) and" +
                    $" the number of special characters ({(options.SpecialSymbols == null ? 0 : options.SpecialSymbols.Count)})";
            
            var ex = Assert.Throws<InvalidOperationException>(() => options.SetLength(futureLength));

            Assert.Equal(message, ex.Message);
        }

        [Fact]
        public void SetLength_FutureLengthIsMoreThanPrevious_LengthIsEqualToFutureLength()
        {
            PasswordOptions options = PasswordOptions.Defaults();
            int futureLength = options.Length + 1;
            int previousLength = options.Length;

            options.SetLength(futureLength);

            Assert.NotEqual(previousLength, options.Length);
            Assert.Equal(futureLength, options.Length);

            Assert.True(options.Length > (options.CountOfCharacters + options.CountOfNumbers + options.SpecialSymbols.Count));
        }
    }
}
