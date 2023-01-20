using PasswordManager.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Domain.RecordEntity.Events
{
    public class UserNameChangedEvent : DomainEvent
    {
        public int RecordId { get; }
        public string UserName { get; }

        public UserNameChangedEvent(int recordId, string userName)
        {
            RecordId = recordId;
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
        }
    }
}
