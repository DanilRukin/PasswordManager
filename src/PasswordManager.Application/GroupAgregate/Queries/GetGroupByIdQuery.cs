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
    public class GetGroupByIdQuery : IRequest<Result<GroupDto>>
    {
        public int Id { get; }
        public bool IncludeAdditionalInformatoion { get; }

        public GetGroupByIdQuery(int id, bool includeAdditionalInformatoion)
        {
            Id = id;
            IncludeAdditionalInformatoion = includeAdditionalInformatoion;
        }
    }
}
