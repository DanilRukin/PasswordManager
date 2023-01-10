using PasswordManager.Domain;
using PasswordManager.Domain.Services;
using PasswordManager.UnitTests.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.UnitTests.Domain
{
    public class PasswordGeneratorTests
    {
        [Fact]
        public void Generate_OnlyCharacters()
        {
            PasswordGenerator generator = new PasswordGenerator();
            PasswordOptions options = PasswordOptions.Defaults();
            options.SetCountOfNumbers(0);

            string password = generator.Generate(options);

            Assert.False(password.Has(c => c >= '0' && c <= '9'));
            Assert.True(password.Length == options.Length);
        }

        [Fact]
        public void Generate_OnlyNumbers()
        {
            PasswordGenerator generator = new PasswordGenerator();
            PasswordOptions options = PasswordOptions.Defaults();
            options.SetCountOfCharacters(0);
            options.SetCountOfNumbers(10);

            string password = generator.Generate(options);

            Assert.False(password.Has(c => c < '0' || c > '9'));
            Assert.True(password.Length == options.Length);
        }

        [Fact]
        public void Generate_OnlySpecialSymbols()
        {
            PasswordGenerator generator = new PasswordGenerator();
            PasswordOptions options = PasswordOptions.Defaults();
            options.SetCountOfCharacters(0);
            options.SetCountOfNumbers(0);
            char[] symbols = new char[]
            {
                '@', '#', '$', '%', '&', '^', '/', '\\', '(', ')',
                '{', '}', '*', '[', ']', ',', '.', ';', '!', '?', '№',
                '~', '`'
            };
            for (int i = 0; i < symbols.Length; i++)
            {
                options.AddSpecialSymbol(symbols[i]);
            }

            string password = generator.Generate(options);

            for (int i = 0; i < symbols.Length; i++)
            {
                Assert.True(password.Contains(symbols[i]));
            }
        }

        [Fact]
        public void Generate_UpperCaseOnly()
        {
            PasswordGenerator generator = new PasswordGenerator();
            PasswordOptions options = PasswordOptions.Defaults();
            options.SetCountOfNumbers(0);
            options.SetUpperCase(true);

            string password = generator.Generate(options);

            foreach (var c in password)
            {
                Assert.True(char.IsUpper(c));
            }
        }

        [Fact]
        public void Generate_LowerCaseOnly()
        {
            PasswordGenerator generator = new PasswordGenerator();
            PasswordOptions options = PasswordOptions.Defaults();
            options.SetCountOfNumbers(0);
            options.SetLowerCase(true);

            string password = generator.Generate(options);

            foreach (var c in password)
            {
                Assert.True(char.IsLower(c));
            }
        }

        [Fact]
        public void Generate_LengthIsMoreThanSumOfDigitsAndCharactersAndSpecialSymbols_ResultAppendedByRandomCharacters()
        {
            PasswordGenerator generator = new PasswordGenerator();
            PasswordOptions options = PasswordOptions.Defaults();
            options.SetCountOfNumbers(0);
            options.SetCountOfCharacters(0);
            Assert.True(options.Length == 0);
            options.SetCountOfNumbers(10);
            options.SetLength(15);

            string password = generator.Generate(options);

            int expectedCharctersCount = options.Length - options.CountOfNumbers;
            int actualCharactersCount = 0;
            for (int i = 0; i < password.Length; i++)
            {
                if (password[i] < '0' || password[i] > '9')
                    actualCharactersCount++;
            }
            Assert.Equal(expectedCharctersCount, actualCharactersCount);
        }
    }
}
