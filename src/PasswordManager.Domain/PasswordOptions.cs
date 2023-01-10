using PasswordManager.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Domain
{
    public class PasswordOptions : ValueObject
    {
        public int Length { get; private set; }
        public int CountOfNumbers { get; private set; }
        public int CountOfCharacters { get; private set; }
        public bool UpperCaseOnly { get; private set; }
        public bool LowerCaseOnly { get; private set; }
        private List<char> _specialSymbols;
        public IReadOnlyCollection<char> SpecialSymbols => _specialSymbols.AsReadOnly();

        private PasswordOptions(int length, int countOfNumbers, int countOfCharacters, bool upperCaseOnly, bool lowerCaseOnly)
        {
            Length = length;
            CountOfNumbers = countOfNumbers;
            CountOfCharacters = countOfCharacters;
            SetUpperCase(upperCaseOnly);
            SetLowerCase(lowerCaseOnly);
            _specialSymbols = new List<char>();
        }


        public void AddSpecialSymbol(char symbol)
        {
            _specialSymbols ??= new List<char>();
            if (!_specialSymbols.Contains(symbol))
            {
                _specialSymbols.Add(symbol);
                Length++;
            }      
            return;
        }

        public void RemoveSpecialSymbol(char symbol)
        {
            if (_specialSymbols?.Remove(symbol) == true)
                Length--;
        }

        public void ClearSpecialSymbols()
        {
            _specialSymbols ??= new List<char>();
            Length -= _specialSymbols.Count;
            _specialSymbols.Clear();
        }

        public void SetLowerCase(bool lowerCase)
        {
            if (UpperCaseOnly && lowerCase)
                throw new InvalidOperationException("Upper case and lower case can not be true at the same time!");
            else
                LowerCaseOnly = lowerCase;
        }

        public void SetUpperCase(bool upperCase)
        {
            if (LowerCaseOnly && upperCase)
                throw new InvalidOperationException("Upper case and lower case can not be true at the same time!");
            else
                UpperCaseOnly = upperCase;
        }

        public void SetCountOfNumbers(int count)
        {
            if (count < 0)
                return;
            Length -= CountOfNumbers;
            CountOfNumbers = count;
            Length += CountOfNumbers;
        }

        public void SetCountOfCharacters(int count)
        {
            if (count < 0)
                return;
            Length -= CountOfCharacters;
            CountOfCharacters = count;
            Length += CountOfCharacters;
        }

        public void SetLength(int length)
        {
            if (length < 1)
                return;
            if (length >= (CountOfCharacters + CountOfNumbers + (_specialSymbols == null ? 0 : _specialSymbols.Count)))
                Length = length;
            else
                throw new InvalidOperationException($"It is not possible to set the length ({length}) less than the sum of the" +
                    $" number of digits ({CountOfNumbers}), the number of characters ({CountOfCharacters}) and" +
                    $" the number of special characters ({(_specialSymbols == null ? 0 : _specialSymbols.Count)})");
        }

        public static PasswordOptions Defaults()
        {
            return new PasswordOptions(8, 2, 6, false, false);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Length;
            yield return CountOfCharacters;
            yield return CountOfNumbers;
            yield return UpperCaseOnly;
            yield return LowerCaseOnly;
        }
    }
}
