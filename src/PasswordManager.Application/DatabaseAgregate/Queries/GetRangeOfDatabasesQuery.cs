using MediatR;
using PasswordManager.Application.Dtos;
using PasswordManager.SharedKernel.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Application.DatabaseAgregate.Queries
{
    public class GetRangeOfDatabasesQuery : IRequest<Result<IEnumerable<DatabaseDto>>>
    {
        public IEnumerable<int> Ids { get; }
        public bool IncludeAdditionalData { get; }

        public GetRangeOfDatabasesQuery(IEnumerable<int> ids, bool includeAdditionalData)
        {
            Ids = ids ?? throw new ArgumentNullException(nameof(ids));
            IncludeAdditionalData = includeAdditionalData;
        }
    }
}
