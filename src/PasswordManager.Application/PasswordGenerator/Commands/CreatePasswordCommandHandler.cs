using MediatR;
using PasswordManager.Application.Dtos;
using PasswordManager.Application.Services;
using PasswordManager.Application.Services.Interfaces;
using PasswordManager.Domain;
using PasswordManager.SharedKernel.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Application.PasswordGenerator.Commands
{
    public class CreatePasswordCommandHandler : IRequestHandler<CreatePasswordCommand, Result<string>>
    {
        private IMapper<PasswordOptionsDto, PasswordOptions> _mapper;

        public CreatePasswordCommandHandler(IMapper<PasswordOptionsDto, PasswordOptions> mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Task<Result<string>> Handle(CreatePasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                PasswordOptions options = _mapper.Map(request.Options);
                Domain.Services.PasswordGenerator generator = new Domain.Services.PasswordGenerator();
                string result = generator.Generate(options);
                return Task.FromResult(Result.Success(result));
            }
            catch (Exception e)
            {
                return Task.FromResult(ExceptionHandler.Handle<string>(e));
            }
        }
    }
}
