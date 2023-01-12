using PasswordManager.Domain.GroupAgregate;
using PasswordManager.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.UnitTests.Fakes
{
    public class FakeGroupFactory : IGroupFactory
    {
        private int _nextId = 1;
        public Group Create(string name)
        {
            Group group = new Group();
            group.ChangeName(name);
            group.GetType()?.GetProperty(nameof(group.Id))?.SetValue(group, _nextId);
            _nextId++;
            return group;
        }
    }
}
