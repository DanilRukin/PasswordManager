using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Domain.Services
{
    public class PasswordGenerator
    {
        public virtual string Generate(PasswordOptions options)
        {
            StringBuilder password = new StringBuilder(options.Length);
            Random random = new Random();
            if (options.CountOfCharacters > 0)
            {
                password.Append(GenerateCharacters(options));
            }
            if (options.CountOfNumbers > 0)
            {
                StringBuilder numbers = new StringBuilder(options.CountOfNumbers);
                for (int i = 0; i < options.CountOfNumbers; i++)
                {
                    numbers.Append(random.Next(0, 9));
                }
                password.Append(numbers);
            }
            if (options.SpecialSymbols != null && options.SpecialSymbols.Count > 0)
            {
                char[] symbols = options.SpecialSymbols.ToArray();
                StringBuilder specialSymbols = new StringBuilder(options.SpecialSymbols.Count);
                for (int i = 0; i < symbols.Length; i++)
                {
                    specialSymbols.Append(symbols[i]);
                }
                password.Append(specialSymbols);
            }
            int difference = options.Length - options.CountOfCharacters - options.CountOfNumbers -
                (options.SpecialSymbols == null ? 0 : options.SpecialSymbols.Count);
            if (difference > 0)
            {
                password.Append(GenerateCharacters(options, difference));
            }
            return Mix(password.ToString());
        }

        private string Mix(string source)
        {
            if (string.IsNullOrWhiteSpace(source))
                return "";
            if (source.Length < 2)
                return source;
            StringBuilder builder = new StringBuilder(source);
            Random random = new Random();
            int index;
            char tmp;
            for (int i = 0; i < source.Length; i++)
            {
                index = random.Next(0, source.Length);
                tmp = builder[i];
                builder[i] = builder[index];
                builder[index] = tmp;
            }
            return builder.ToString();
        }

        private string GenerateCharacters(PasswordOptions options, int prefferedCount = 0)
        {
            Random random = new Random();
            int count = prefferedCount > 0 ? prefferedCount : options.CountOfCharacters;
            if (count > 0)
            {
                StringBuilder characters = new StringBuilder(count);
                for (int i = 0; i < count; i++)
                {
                    if (options.UpperCaseOnly)
                        characters.Append((char)random.Next('A', 'Z'));
                    else if (options.LowerCaseOnly)
                        characters.Append((char)random.Next('a', 'z'));
                    else
                    {
                        if (random.Next() % 2 == 0)
                            characters.Append((char)random.Next('A', 'Z'));
                        else
                            characters.Append((char)random.Next('a', 'z'));
                    }
                }
                return characters.ToString();
            }
            return "";
        }
    }
}
