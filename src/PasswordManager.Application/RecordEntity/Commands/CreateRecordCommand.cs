using MediatR;
using PasswordManager.Application.Dtos;
using PasswordManager.SharedKernel.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Application.RecordEntity.Commands
{
    public class CreateRecordCommand : IRequest<Result<RecordDto>>
    {
        public RecordDto Record { get; }

        public CreateRecordCommand(RecordDto record)
        {
            Record = record ?? throw new ArgumentNullException(nameof(record));
        }
    }
}
