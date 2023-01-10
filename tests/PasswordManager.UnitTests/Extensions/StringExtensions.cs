using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.UnitTests.Extensions
{
    public static class StringExtensions
    {
        public static bool Has(this string str, Func<char, bool> predicate)
        {
            foreach (char c in str)
            {
                if (predicate(c))
                    return true;
            }
            return false;
        }
    }
}
