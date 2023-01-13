using PasswordManager.Domain.DatabaseAgregate;
using PasswordManager.Domain.GroupAgregate;
using PasswordManager.Domain.Services;
using PasswordManager.UnitTests.Fakes;

namespace PasswordManager.UnitTests.Domain
{
    public class DatabaseTests
    {
        private IDatabaseFactory _databaseFactory;
        private IGroupFactory _groupFactory;

        public DatabaseTests()
        {
            _databaseFactory = new FakeDatabaseFactory();
            _groupFactory = new FakeGroupFactory();
        }

        [Fact]
        public void AddGroup_GroupIsOk_GroupWasAdded()
        {
            Database db = _databaseFactory.Create("name");
            Assert.True(db.Id > 0);
            Group group = _groupFactory.Create("group");
            Assert.True(group.Id > 0);

            db.AddGroup(group);

            Assert.NotNull(group.ParentDatabase);
            Assert.Equal(group.ParentDatabase.Id, db.Id);
            Assert.Contains(group, db.Groups);
        }

        [Fact]
        public void AddGroup_GroupIsNull_GroupWasNotAdded()
        {
            Database db = _databaseFactory.Create("name");
            Group group = null;

            db.AddGroup(group);

            Assert.Empty(db.Groups);
        }

        [Fact]
        public void RemoveGroup_GroupWasRemoved()
        {
            Database db = _databaseFactory.Create("name");
            Group firstGroup = _groupFactory.Create("group");
            Group secondGroup = _groupFactory.Create("group");
            db.AddGroup(firstGroup);
            db.AddGroup(secondGroup);

            db.RemoveGroup(firstGroup);

            Assert.Single(db.Groups);
            Assert.Contains(secondGroup, db.Groups);
            Assert.Null(firstGroup.ParentDatabase);
        }
    }
}
