using PasswordManager.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Domain.RecordEntity.Events
{
    public class LastModifiedDateChangedEvent : DomainEvent
    {
        public int Id { get; }
        public DateTime LastModifiedDate { get; set; }

        public LastModifiedDateChangedEvent(int id, DateTime lastModifiedDate)
        {
            Id = id;
            LastModifiedDate = lastModifiedDate;
        }
    }
}
