using MediatR;
using Microsoft.EntityFrameworkCore;
using PasswordManager.Application.Dtos;
using PasswordManager.Application.Services;
using PasswordManager.Application.Services.Interfaces;
using PasswordManager.Data;
using PasswordManager.Domain.DatabaseAgregate;
using PasswordManager.SharedKernel.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Application.DatabaseAgregate.Commands
{
    public class UpdateDatabaseDataCommandHandler : IRequestHandler<UpdateDatabaseDataCommand, Result<DatabaseDto>>
    {
        private PasswordManagerDbContext _context;
        private IMapper<Database, DatabaseDto> _mapper;

        public UpdateDatabaseDataCommandHandler(PasswordManagerDbContext context, IMapper<Database, DatabaseDto> mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<DatabaseDto>> Handle(UpdateDatabaseDataCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Database? database = await _context.Databases
                    .FirstOrDefaultAsync(d => d.Id == request.Database.Id, cancellationToken);
                if (database == null)
                    return Result<DatabaseDto>.NotFound($"No such database with id: {request.Database.Id}");
                database.ChangeName(request.Database.Name);
                await _context.SaveEntitiesAsync(cancellationToken);
                return Result.Success(_mapper.Map(database));
            }
            catch (Exception e)
            {
                return ExceptionHandler.Handle<DatabaseDto>(e);
            }
        }
    }
}
