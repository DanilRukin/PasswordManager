using Microsoft.EntityFrameworkCore;
using PasswordManager.Domain.DatabaseAgregate;
using PasswordManager.Domain.GroupAgregate;
using PasswordManager.Domain.RecordEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Data
{
    public static class PasswordManagerDbContextExtensions
    {
        public static IQueryable<Database> IncludeGroups(this IQueryable<Database> databases)
        {
            return databases.Include(DataConstants.GroupsCollectionName);
        }

        public static IQueryable<Group> IncludeRecords(this IQueryable<Database> databases)
        {
            return databases.Include(DataConstants.RecordsCollectionName);
        }

        public static IQueryable<Group> IncludeRecords(this IQueryable<Group> groups)
        {
            return groups.Include(DataConstants.RecordsCollectionName);
        }

        public static IQueryable<Group> IncludeParentDatabase(this IQueryable<Group> groups)
        {
            return groups.Include(g => g.ParentDatabase);
        }

        public static IQueryable<Record> IncludeContainer(this IQueryable<Record> records)
        {
            return records.Include(r => r.RecordContainerId);
        }
    }
}
