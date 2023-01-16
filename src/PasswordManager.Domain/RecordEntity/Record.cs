using PasswordManager.Domain.RecordEntity.Events;
using PasswordManager.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Domain.RecordEntity
{
    public class Record : EntityBase<int>
    {
        public DateTime CreationDate { get; private set; }
        public DateTime LastModifiedDate { get; private set; }
        public DateTime LastAccessDate { get; private set; }
        public string Description { get; private set; }
        public string ResourceName { get; private set; }
        public string ResourcePasswordHash { get; private set; }
        public string ResourceUrl { get; private set; }

        public int? RecordContainerId { get; private set; }

        public Record()
        {
            Description = "";
            ResourceName = "";
            ResourcePasswordHash = "";
            ResourceUrl = "";
            DateTime now = DateTime.UtcNow;
            CreationDate = now;
            LastAccessDate = now;
            LastModifiedDate = now;
        }

        public void ChangeDescription(string description)
        {
            if (description == null)
                return;
            Description = description;
            AddDomainEvent(new DescriptionChangedEvent(Id, description));
        }

        public void ChangeResourceName(string resourceName)
        {
            if (string.IsNullOrWhiteSpace(resourceName))
                return;
            ResourceName = resourceName;
            AddDomainEvent(new ResourceNameChangedEvent(Id, resourceName));
        }

        public void ChangeResourcePasswordHash(string passwordHash)
        {
            if (passwordHash == null)
                return;
            ResourcePasswordHash = passwordHash;
            AddDomainEvent(new PasswordHashChangedEvent(Id, passwordHash));
        }

        public void ChangeResourceUrl(string url)
        {
            if (url == null)
                return;
            ResourceUrl = url;
            AddDomainEvent(new ResourceUrlChangedEvent(Id, url));
        }

        public void ChangeLastAccessDate(DateTime lastAccessDate)
        {
            if (lastAccessDate <= CreationDate || lastAccessDate <= LastAccessDate || lastAccessDate < LastModifiedDate)
                return;
            LastAccessDate = lastAccessDate;
            AddDomainEvent(new LastAccessDateChangedEvent(Id, lastAccessDate));
        }

        public void ChangeLastModifiedDate(DateTime lastModifiedDate)
        {
            if (lastModifiedDate <= CreationDate || lastModifiedDate <= LastModifiedDate || lastModifiedDate < LastAccessDate)
                return;
            LastModifiedDate = lastModifiedDate;
            AddDomainEvent(new LastModifiedDateChangedEvent(Id, lastModifiedDate));
        }

        internal void ChangeContainerId(int id)
        {
            RecordContainerId = id;
        }
    }
}
