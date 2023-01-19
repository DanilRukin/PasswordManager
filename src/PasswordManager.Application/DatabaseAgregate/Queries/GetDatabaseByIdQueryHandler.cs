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

namespace PasswordManager.Application.DatabaseAgregate.Queries
{
    public class GetDatabaseByIdQueryHandler : IRequestHandler<GetDatabaseByIdQuery, Result<DatabaseDto>>
    {
        private PasswordManagerDbContext _context;
        private IMapper<Database, DatabaseDto> _mapper;

        public GetDatabaseByIdQueryHandler(PasswordManagerDbContext context, IMapper<Database, DatabaseDto> mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<DatabaseDto>> Handle(GetDatabaseByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Database? database;
                if (request.IncludeAdditionalData)
                    database = await _context.Databases
                        .IncludeGroups()
                        .IncludeRecords()
                        .FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken);
                else
                    database = await _context.Databases
                        .FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken);
                if (database == null)
                    return Result<DatabaseDto>.NotFound($"No such database with id: {request.Id}");
                return Result.Success(_mapper.Map(database));
            }
            catch (Exception e)
            {
                return ExceptionHandler.Handle<DatabaseDto>(e);
            }
        }
    }
}
