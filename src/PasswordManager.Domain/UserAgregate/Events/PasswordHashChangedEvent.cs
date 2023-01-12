using PasswordManager.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Domain.UserAgregate.Events
{
    public class PasswordHashChangedEvent : DomainEvent
    {
        public int Id { get; }
        public string PasswordHash { get; }

        public PasswordHashChangedEvent(int id, string passwordHash)
        {
            Id = id;
            PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
        }
    }
}
