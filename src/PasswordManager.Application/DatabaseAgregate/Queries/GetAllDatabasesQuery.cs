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
    public class GetAllDatabasesQuery : IRequest<Result<IEnumerable<DatabaseDto>>>
    {
        public bool IncludeAddtionalData { get; }

        public GetAllDatabasesQuery(bool includeAddtionalData)
        {
            IncludeAddtionalData = includeAddtionalData;
        }
    }
}
