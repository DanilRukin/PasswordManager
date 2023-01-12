using PasswordManager.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Domain.RecordEntity.Events
{
    public class LastAccessDateChangedEvent : DomainEvent
    {
        public int Id { get; }
        public DateTime LastAccessDate { get; }

        public LastAccessDateChangedEvent(int id, DateTime lastAccessDate)
        {
            Id = id;
            LastAccessDate = lastAccessDate;
        }
    }
}
