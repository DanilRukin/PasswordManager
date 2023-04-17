using PasswordManager.Application.Dtos;
using PasswordManager.Application.Services.Interfaces;
using PasswordManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Application.Services.Mappers
{
    internal class PasswordOptionsDtoToPasswordOptionsMapper : IMapper<PasswordOptionsDto, PasswordOptions>
    {
        public PasswordOptions Map(PasswordOptionsDto source)
        {
            var options = PasswordOptions.Defaults();
            foreach (var item in source.SpecialSymbols)
            {
                options.AddSpecialSymbol(item);
            }
            options.SetCountOfCharacters(source.CountOfCharacters);
            options.SetCountOfNumbers(source.CountOfNumbers);
            options.SetLowerCase(source.LowerCaseOnly);
            options.SetUpperCase(source.UpperCaseOnly);
            options.SetLength(source.Length);
            return options;
        }
    }
}
