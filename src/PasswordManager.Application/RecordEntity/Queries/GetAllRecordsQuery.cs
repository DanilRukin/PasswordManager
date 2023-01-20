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
    public class GetAllRecordsQuery : IRequest<Result<IEnumerable<RecordDto>>>
    {
        public bool IncludeAdditionalData { get; }

        public GetAllRecordsQuery(bool includeAdditionalData)
        {
            IncludeAdditionalData = includeAdditionalData;
        }
    }
}
