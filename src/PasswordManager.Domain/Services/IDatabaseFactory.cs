using PasswordManager.Domain.DatabaseAgregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Domain.Services
{
    public interface IDatabaseFactory
    {
        Database Create(string name);
    }
}
