using PasswordManager.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Domain.DatabaseAgregate.Events
{
    public class GroupRemovedEvent : DomainEvent
    {
        public int DatabaseId { get; }
        public int GropId { get; }

        public GroupRemovedEvent(int databaseId, int gropId)
        {
            DatabaseId = databaseId;
            GropId = gropId;
        }
    }
}
