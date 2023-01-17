using Microsoft.EntityFrameworkCore;
using PasswordManager.Data;
using PasswordManager.Domain.DatabaseAgregate;
using PasswordManager.Domain.GroupAgregate;
using PasswordManager.Domain.RecordEntity;
using PasswordManager.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Record = PasswordManager.Domain.RecordEntity.Record;

namespace PasswordManager.UnitTests.Data
{
    public class PasswordManagerDbContextTests : BaseEfTestFixture
    {
        [Fact]
        public void AddDatabase_DatabaseAdded()
        {
            var db = CreateDatabase();
            db.Database.EnsureCreated();
            Database database = ((IDatabaseFactory)db).Create("database");
            Assert.NotEmpty(db.Databases);
            Assert.Contains(database, db.Databases);
            Assert.True(database.Id > 0);
            CloseDatabase(db);
        }

        [Fact]
        public void AddGroupToDatabase_GroupAdded()
        {
            var db = CreateDatabase();
            db.Database.EnsureCreated();
            Database database = ((IDatabaseFactory)db).Create("database");
            var group = db.Create("group");
            Assert.NotEmpty(db.Groups);
            Assert.Contains(group, db.Groups);
            Assert.True(group.Id > 0);
            database.AddGroup(group);
            db.SaveChanges();
            db.Entry(database).State = EntityState.Detached;
            var existingDatabase = db.Databases.IncludeGroups().FirstOrDefault(d => d.Id == database.Id);
            Assert.NotNull(existingDatabase);
            Assert.NotSame(database, existingDatabase);
            Assert.Contains(group, existingDatabase.Groups);
            CloseDatabase(db);
        }

        [Fact]
        public void AddRecordToDatabase_RecordAdded()
        {
            var db = CreateDatabase();
            db.Database.EnsureCreated();
            Database database = ((IDatabaseFactory)db).Create("database");
            Record record = db.Create("", "", DateTime.UtcNow);
            Assert.NotEmpty(db.Records);
            Assert.Contains(record, db.Records);
            Assert.True(record.Id > 0);
            database.AddRecord(record);
            db.SaveChanges();
            db.Entry(database).State = EntityState.Detached;
            var existingDatabase = db.Databases.IncludeRecords().FirstOrDefault(d => d.Id == database.Id);
            Assert.NotNull(existingDatabase);
            Assert.NotSame(database, existingDatabase);
            Assert.Contains(record, existingDatabase.Records);
            CloseDatabase(db);
        }

        [Fact]
        public void AddRecordToGroup_RecordAdded()
        {
            var db = CreateDatabase();
            db.Database.EnsureCreated();
            Group group = db.Create("group");
            Record record = db.Create("", "", DateTime.UtcNow);
            Assert.NotEmpty(db.Records);
            Assert.Contains(record, db.Records);
            Assert.True(record.Id > 0);
            group.AddRecord(record);
            db.SaveChanges();
            db.Entry(group).State = EntityState.Detached;
            var existingGroup = db.Groups.IncludeRecords().FirstOrDefault(d => d.Id == group.Id);
            Assert.NotNull(existingGroup);
            Assert.NotSame(group, existingGroup);
            Assert.Contains(record, existingGroup.Records);
            CloseDatabase(db);
        }

        [Fact]
        public void RemoveGroupWithSeveralRecords_RecordsShuoldBeRemoved()
        {
            var db = CreateDatabase();
            db.Database.EnsureCreated();
            Group group = db.Create("group");
            var firstRecord = db.Create("resource 1", "", DateTime.UtcNow);
            var secondRecord = db.Create("resource 2", "", DateTime.UtcNow);
            group.AddRecord(firstRecord);
            group.AddRecord(secondRecord);
            db.SaveChanges();
            Assert.NotEmpty(db.Records);
            Assert.NotEmpty(db.Groups);

            db.Entry(group).State = EntityState.Detached;
            var existingGroup = db.Groups.FirstOrDefault(g => g.Id == group.Id);
            Assert.NotNull(existingGroup);
            Assert.NotSame(existingGroup, group);
            Assert.Empty(existingGroup.Records);

            db.Groups.Remove(existingGroup);
            db.SaveChanges();

            Assert.Empty(db.Groups);
            Assert.Empty(db.Records);

            CloseDatabase(db);
        }

        [Fact]
        public void RemoveDatabaseWithGroupThatHasRecordsAndWithRecords_DatabaseShouldBeClear()
        {
            var db = CreateDatabase();
            db.Database.EnsureCreated();
            Database database = ((IDatabaseFactory)db).Create("database");
            Group group = db.Create("group");
            var firstRecord = db.Create("resource 1", "", DateTime.UtcNow);
            var secondRecord = db.Create("resource 2", "", DateTime.UtcNow);
            var recordInDatabase = db.Create("in database", "", DateTime.UtcNow);
            group.AddRecord(firstRecord);
            group.AddRecord(secondRecord);
            database.AddRecord(recordInDatabase);
            database.AddGroup(group);
            db.SaveChanges();
            Assert.NotEmpty(db.Records);
            Assert.NotEmpty(db.Groups);
            Assert.NotEmpty(db.Databases);

            db.Entry(database).State = EntityState.Detached;
            var existingDatabase = db.Databases.FirstOrDefault(d => d.Id == database.Id);
            Assert.NotNull(existingDatabase);
            Assert.NotSame(existingDatabase, database);
            Assert.Empty(existingDatabase.Records);
            Assert.Empty(existingDatabase.Groups);

            db.Groups.Remove(existingDatabase);
            db.SaveChanges();

            Assert.Empty(db.Groups);
            Assert.Empty(db.Records);
            Assert.Empty(db.Databases);

            CloseDatabase(db);
        }
    }
}
