using MediatR;
using PasswordManager.Application.Dtos;
using PasswordManager.SharedKernel.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Application.GroupAgregate.Queries
{
    public class GetRangeOfGroupsQuery : IRequest<Result<IEnumerable<GroupDto>>>
    {
        public IEnumerable<int> Ids { get; }
        public bool IncludeAdditionalData { get; }

        public GetRangeOfGroupsQuery(IEnumerable<int> ids, bool includeAdditionalData)
        {
            Ids = ids ?? throw new ArgumentNullException(nameof(ids));
            IncludeAdditionalData = includeAdditionalData;
        }
    }
}
