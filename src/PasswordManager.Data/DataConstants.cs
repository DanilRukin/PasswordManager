using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Data
{
    public static class DataConstants
    {
        public const string RecordsCollectionName = "_records";
        public const string GroupAndDatabaseCommonTableName = "RecordContainers";
        public const string DiscriminatorName = "ContainerType";
        public const string GroupsCollectionName = "_groups";
    }
}
