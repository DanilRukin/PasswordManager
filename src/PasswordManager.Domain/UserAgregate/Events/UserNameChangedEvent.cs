using PasswordManager.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Domain.UserAgregate.Events
{
    public class UserNameChangedEvent : DomainEvent
    {
        public int Id { get; }
        public string UserName { get; }

        public UserNameChangedEvent(int id, string userName)
        {
            Id = id;
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
        }
    }
}
