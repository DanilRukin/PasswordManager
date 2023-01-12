using PasswordManager.Domain.GroupAgregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Domain.Services
{
    public interface IGroupFactory
    {
        Group Create(string name);
    }
}
