using PasswordManager.Domain.UserAgregate.Events;
using PasswordManager.SharedKernel;
using PasswordManager.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Domain.UserAgregate
{
    public class User : EntityBase<int>, IAgregateRoot
    {
        public string UserName { get; private set; }
        public string PasswordHash { get; private set; }

        public void ChangeUserName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new InvalidOperationException("User name is null or empty");
            UserName = userName;
            AddDomainEvent(new UserNameChangedEvent(Id, userName));
        }

        public void ChangePasswordHash(string passwordHash)
        {
            if (passwordHash == null)
                throw new InvalidOperationException("Password hash is null"); ;
            PasswordHash = passwordHash;
            AddDomainEvent(new PasswordHashChangedEvent(Id, passwordHash));
        }
    }
}
