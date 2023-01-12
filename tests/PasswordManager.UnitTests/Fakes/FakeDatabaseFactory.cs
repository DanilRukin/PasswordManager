using PasswordManager.Domain.DatabaseAgregate;
using PasswordManager.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.UnitTests.Fakes
{
    public class FakeDatabaseFactory : IDatabaseFactory
    {
        private int _nextId = 1;
        public Database Create(string name)
        {
            Database db = new Database();
            db.ChangeName(name);
            db.GetType()?.GetProperty(nameof(db.Id))?.SetValue(db, _nextId);
            _nextId++;
            return db;
        }
    }
}
