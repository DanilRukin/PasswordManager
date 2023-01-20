using MediatR;
using PasswordManager.SharedKernel.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Application.RecordEntity.Commands
{
    public class DeleteRecordCommand : IRequest<Result>
    {
        public int Id { get; }

        public DeleteRecordCommand(int id)
        {
            Id = id;
        }
    }
}
