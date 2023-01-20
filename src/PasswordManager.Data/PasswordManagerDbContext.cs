using Microsoft.EntityFrameworkCore;
using PasswordManager.Domain.DatabaseAgregate;
using PasswordManager.Domain.GroupAgregate;
using PasswordManager.Domain.RecordEntity;
using PasswordManager.Domain.Services;
using PasswordManager.SharedKernel;
using PasswordManager.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PasswordManager.Data
{
    public class PasswordManagerDbContext : DbContext, IDatabaseFactory, IGroupFactory, IRecordFactory
    {
        private IDomainEventDispatcher _domainEventDispatcher;

        public PasswordManagerDbContext(IDomainEventDispatcher domainEventDispatcher,
            DbContextOptions<PasswordManagerDbContext> options) : base(options)
        {
            _domainEventDispatcher = domainEventDispatcher;
        }

        public DbSet<Database> Databases { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Record> Records { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public Record Create(string resourceName, string resourcePasswordHash, DateTime creationDate, string resourceUrl = "", string description = "", string userName = "")
        {
            Record record = new Record();
            record.ChangeResourceName(resourceName);
            record.ChangeResourcePasswordHash(resourcePasswordHash);
            record.ChangeResourceUrl(resourceUrl);
            record.ChangeDescription(description);
            record.ChangeUserName(userName);
            record.GetType()?.GetProperty(nameof(record.CreationDate))?.SetValue(record, creationDate);
            record.GetType()?.GetProperty(nameof(record.LastAccessDate))?.SetValue(record, creationDate);
            record.GetType()?.GetProperty(nameof(record.LastModifiedDate))?.SetValue(record, creationDate);
            Records.Add(record);
            SaveChanges();
            return record;
        }

        public Group Create(string name)
        {
            Group group = new Group();
            group.ChangeName(name);
            Groups.Add(group);
            SaveChanges();
            return group;
        }

        Database IDatabaseFactory.Create(string name)
        {
            PasswordManager.Domain.DatabaseAgregate.Database db = new();
            db.ChangeName(name);
            Databases.Add(db);
            SaveChanges();
            return db;
        }

        public async Task SaveEntitiesAsync(CancellationToken cancellationToken)
        {
            var result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            var events = ChangeTracker.Entries<IDomainObject>()
                .Select(e => e.Entity)
                .Where(e => e.DomainEvents.Any())
                .ToArray();
            await _domainEventDispatcher.DispatchAndClearEvents(events);
        }
    }
}
