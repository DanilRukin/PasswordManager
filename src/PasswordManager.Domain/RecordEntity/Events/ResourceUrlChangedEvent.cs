using PasswordManager.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Domain.RecordEntity.Events
{
    public class ResourceUrlChangedEvent : DomainEvent
    {
        public int Id { get; }
        public string Url { get; }

        public ResourceUrlChangedEvent(int id, string url)
        {
            Id = id;
            Url = url ?? throw new ArgumentNullException(nameof(url));
        }
    }
}
