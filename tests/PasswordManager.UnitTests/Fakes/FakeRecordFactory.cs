using PasswordManager.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.UnitTests.Fakes
{
    public class FakeRecordFactory : IRecordFactory
    {
        private int _nextId = 1;
        public PasswordManager.Domain.RecordEntity.Record Create(string resourceName, string resourcePasswordHash,
            DateTime creationDate, string resourceUrl = "", string description = "")
        {
            PasswordManager.Domain.RecordEntity.Record record = new PasswordManager.Domain.RecordEntity.Record();
            record.GetType()?.GetProperty(nameof(record.Id))?.SetValue(record, _nextId);
            _nextId++;
            record.ChangeResourceName(resourceName);
            record.ChangeResourcePasswordHash(resourcePasswordHash);
            record.ChangeResourceUrl(resourceUrl);
            record.ChangeDescription(description);
            record.GetType()?.GetProperty(nameof(record.CreationDate))?.SetValue(record, creationDate);
            record.GetType()?.GetProperty(nameof(record.LastAccessDate))?.SetValue(record, creationDate);
            record.GetType()?.GetProperty(nameof(record.LastModifiedDate))?.SetValue(record, creationDate);
            return record;
        }
    }
}
