using PasswordManager.Domain.DatabaseAgregate;
using PasswordManager.Domain.GroupAgregate;
using PasswordManager.Domain.Services;
using PasswordManager.UnitTests.Fakes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Record = PasswordManager.Domain.RecordEntity.Record;

namespace PasswordManager.UnitTests.Domain
{
    public class GroupTests
    {
        private IGroupFactory _groupFactory;
        private IRecordFactory _recordFactory;
        private IDatabaseFactory _databaseFactory;

        public GroupTests()
        {
            _groupFactory = new FakeGroupFactory();
            _recordFactory = new FakeRecordFactory();
            _databaseFactory = new FakeDatabaseFactory();
        }

        [Fact]
        public void AddRecord_RecordWasAdded()
        {
            Group group = _groupFactory.Create("group");
            Record record = _recordFactory.Create("", "", DateTime.UtcNow);

            group.AddRecord(record);

            Assert.Equal(group.Id, record.RecordContainerId);
            Assert.NotNull(group.Records);
            Assert.Contains(record, group.Records);
        }

        [Fact]
        public void RemoveRecord_RecordWasRemoved()
        {
            Group group = _groupFactory.Create("group");
            Record record = _recordFactory.Create("", "", DateTime.UtcNow);
            group.AddRecord(record);

            group.RemoveRecord(record);

            Assert.Empty(group.Records);
            Assert.Equal(0, record.RecordContainerId);
        }

        [Fact]
        public void RemoveRecord_GroupWasInDatabase_RecordRemovedFromGroupButAddedToTheDatabase()
        {
            Database db = _databaseFactory.Create("Database");
            Group group = _groupFactory.Create("group");
            Record record = _recordFactory.Create("", "", DateTime.UtcNow);
            group.AddRecord(record);
            db.AddGroup(group);

            group.RemoveRecord(record);

            Assert.Empty(group.Records);
            Assert.Equal(db.Id, record.RecordContainerId);
            Assert.NotEmpty(db.Records);
            Assert.Contains(record, db.Records);
        }

        [Fact]
        public void AddRecordToAnotherGroup_ButRecordAlreadyHasAContainer_ThrowsInvalidOperationExceptionWithMessage()
        {
            Group firstGroup = _groupFactory.Create("group");
            Group secondGroup = _groupFactory.Create("group");
            Record record = _recordFactory.Create("", "", DateTime.UtcNow);
            firstGroup.AddRecord(record);
            string message = "Unable to add record because it's already has a container.";

            var ex = Assert.Throws<InvalidOperationException>(() => secondGroup.AddRecord(record));
            Assert.NotNull(ex);
            Assert.Equal(message, ex.Message);
        }
    }
}
