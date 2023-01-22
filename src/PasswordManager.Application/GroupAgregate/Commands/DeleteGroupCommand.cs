using MediatR;
using PasswordManager.SharedKernel.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Application.GroupAgregate.Commands
{
    public class DeleteGroupCommand : IRequest<Result>
    {
        public int Id { get; }

        public DeleteGroupCommand(int id)
        {
            Id = id;
        }
    }
}
