using PasswordManager.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Domain.RecordEntity.Events
{
    public class DescriptionChangedEvent : DomainEvent
    {
        public string Description { get; }
        public int Id { get; }

        public DescriptionChangedEvent(int id, string description)
        {
            Id = id;
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }
    }
}
