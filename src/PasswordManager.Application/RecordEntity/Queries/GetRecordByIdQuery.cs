using MediatR;
using PasswordManager.Application.Dtos;
using PasswordManager.SharedKernel.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Application.RecordEntity.Queries
{
    public class GetRecordByIdQuery : IRequest<Result<RecordDto>>
    {
        public int Id { get; }
        public bool IncludeAdditionalData { get; }

        public GetRecordByIdQuery(int id, bool includeAdditionalData)
        {
            Id = id;
            IncludeAdditionalData = includeAdditionalData;
        }
    }
}
