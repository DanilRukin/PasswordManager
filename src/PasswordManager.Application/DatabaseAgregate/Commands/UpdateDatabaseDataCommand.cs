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
    public class UpdateDatabaseDataCommand : IRequest<Result<DatabaseDto>>
    {
        public DatabaseDto Database { get; }

        public UpdateDatabaseDataCommand(DatabaseDto database)
        {
            Database = database ?? throw new ArgumentNullException(nameof(database));
        }
    }
}
