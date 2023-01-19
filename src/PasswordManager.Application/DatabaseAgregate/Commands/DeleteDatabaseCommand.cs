using MediatR;
using PasswordManager.SharedKernel.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Application.DatabaseAgregate.Commands
{
    public class DeleteDatabaseCommand : IRequest<Result>
    {
        public int DatabaseId { get; }

        public DeleteDatabaseCommand(int databaseId)
        {
            DatabaseId = databaseId;
        }
    }
}
