using MediatR;
using PasswordManager.Application.Dtos;
using PasswordManager.SharedKernel.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Application.DatabaseAgregate.Commands
{
    public class CreateDatabaseCommand : IRequest<Result<DatabaseDto>>
    {
        public DatabaseDto Database { get; }

        public CreateDatabaseCommand(DatabaseDto database)
        {
            Database = database ?? throw new ArgumentNullException(nameof(database));
        }
    }
}
