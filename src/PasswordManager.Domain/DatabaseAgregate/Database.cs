using PasswordManager.Domain.DatabaseAgregate.Events;
using PasswordManager.Domain.GroupAgregate;
using PasswordManager.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Domain.DatabaseAgregate
{
    public class Database : Group, IAgregateRoot
    {
        protected List<Group> _groups = new List<Group>();
        public IReadOnlyCollection<Group> Groups => _groups.AsReadOnly();

        public virtual void AddGroup(Group group)
        {
            if (group == null)
                return;
            _groups ??= new List<Group>();
            if (!_groups.Contains(group))
            {
                group.AddInDatabase(this);
                _groups.Add(group);
                AddDomainEvent(new GroupAddedEvent(Id, group.Id));
            }                
        }

        public virtual void RemoveGroup(Group group)
        {
            if (group != null)
            {
                _groups?.Remove(group);
                group.RemoveFromDatabase();
                AddDomainEvent(new GroupRemovedEvent(Id, group.Id));
            }        
        }
    }
}
