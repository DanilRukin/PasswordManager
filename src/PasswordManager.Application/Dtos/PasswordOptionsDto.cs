using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Application.Dtos
{
    public class PasswordOptionsDto
    {
        public int Length { get; set; }
        public int CountOfNumbers { get; set; }
        public int CountOfCharacters { get; set; }
        public bool UpperCaseOnly { get; set; }
        public bool LowerCaseOnly { get; set; }
        public List<char> SpecialSymbols { get; set; } = new List<char>();
    }
}
