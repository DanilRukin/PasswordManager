using PasswordManager.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Domain.DatabaseAgregate.Events
{
    public class GroupAddedEvent : DomainEvent
    {
        public int DatabaseId { get; }
        public int GroupId { get; }

        public GroupAddedEvent(int databaseId, int groupId)
        {
            DatabaseId = databaseId;
            GroupId = groupId;
        }
    }
}
