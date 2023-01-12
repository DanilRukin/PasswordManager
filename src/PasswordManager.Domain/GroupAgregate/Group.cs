using PasswordManager.Domain.DatabaseAgregate;
using PasswordManager.Domain.RecordEntity;
using PasswordManager.SharedKernel;
using PasswordManager.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Domain.GroupAgregate
{
    public class Group : EntityBase<int>, IAgregateRoot
    {
        public Database? ParentDatabase { get; private set; }
        public string Name { get; private set; }

        protected List<Record> _records = new List<Record>();
        public IReadOnlyCollection<Record> Records => _records.AsReadOnly();

        public virtual void AddRecord(Record record)
        {
            if (record == null)
                return;
            _records ??= new List<Record>();
            if (!_records.Contains(record))
            {
                record.ChangeContainerId(Id);
                _records.Add(record);
            }
        }

        public virtual void RemoveRecord(Record record)
        {
            if (record != null)
            {
                if (_records?.Remove(record) == true)
                    record.ChangeContainerId(0);
            }
        }

        internal void AddInDatabase(Database database)
        {
            ParentDatabase = database;
        }

        internal void RemoveFromDatabase()
        {
            ParentDatabase = null;
        }

        public void ChangeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidOperationException("Name was null");
            Name = name;
        }
    }
}
