using MediatR;
using PasswordManager.Application.Dtos;
using PasswordManager.SharedKernel.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Application.PasswordGenerator.Commands
{
    public class CreatePasswordCommand : IRequest<Result<string>>
    {
        public PasswordOptionsDto Options { get; }

        public CreatePasswordCommand(PasswordOptionsDto options)
        {
            Options = options ?? throw new ArgumentNullException(nameof(options));
        }
    }
}
