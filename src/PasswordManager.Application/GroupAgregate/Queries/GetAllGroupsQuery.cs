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
    public class GetAllGroupsQuery : IRequest<Result<IEnumerable<GroupDto>>>
    {
        public bool IncludeAdditionalData { get; }

        public GetAllGroupsQuery(bool includeAdditionalData)
        {
            IncludeAdditionalData = includeAdditionalData;
        }
    }
}
