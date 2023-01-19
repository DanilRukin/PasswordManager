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
using System.Threading;
using System.Threading.Tasks;

namespace PasswordManager.Application.DatabaseAgregate.Queries
{
    public class GetAllDatabasesQueryHandler : IRequestHandler<GetAllDatabasesQuery, Result<IEnumerable<DatabaseDto>>>
    {
        private PasswordManagerDbContext _context;
        private IMapper<Database, DatabaseDto> _mapper;

        public GetAllDatabasesQueryHandler(PasswordManagerDbContext context, IMapper<Database, DatabaseDto> mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<IEnumerable<DatabaseDto>>>Handle(GetAllDatabasesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<Database>? databases;
                if (request.IncludeAddtionalData)
                {
                    databases = await _context.Databases
                        .IncludeGroups()
                        .IncludeRecords()
                        .ToListAsync(cancellationToken);                    
                }
                else
                {
                    databases = await _context.Databases
                        .ToListAsync(cancellationToken);
                }
                if (databases == null || !databases.Any())
                    return Result<IEnumerable<DatabaseDto>>.NotFound("No databases in storage");
                else
                    return Result.Success(databases.Select(db => _mapper.Map(db)));
            }
            catch (Exception e)
            {
                return ExceptionHandler.Handle<IEnumerable<DatabaseDto>>(e);
            }
        }
    }
}
