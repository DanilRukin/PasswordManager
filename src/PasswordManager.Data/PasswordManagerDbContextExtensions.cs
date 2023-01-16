using Microsoft.EntityFrameworkCore;
using PasswordManager.Domain.DatabaseAgregate;
using PasswordManager.Domain.GroupAgregate;
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
    }
}
