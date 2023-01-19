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
    public class GetRangeOfDatabasesQueryHandler : IRequestHandler<GetRangeOfDatabasesQuery, Result<IEnumerable<DatabaseDto>>>
    {
        private PasswordManagerDbContext _context;
        private IMapper<Database, DatabaseDto> _mapper;

        public GetRangeOfDatabasesQueryHandler(PasswordManagerDbContext context, IMapper<Database, DatabaseDto> mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<IEnumerable<DatabaseDto>>> Handle(GetRangeOfDatabasesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Database? database;
                List<Database> databases = new List<Database>();
                foreach (int id in request.Ids)
                {
                    database = await GetDatabase(id, request.IncludeAdditionalData, cancellationToken);
                    if (database == null)
                        return Result<IEnumerable<DatabaseDto>>.NotFound($"No such database with id: {id}");
                    databases.Add(database);
                }
                if (databases.Any())
                    return Result.Success(databases.Select(db => _mapper.Map(db)));
                else
                    return Result<IEnumerable<DatabaseDto>>.NotFound("No databases in storage");
            }
            catch (Exception e)
            {
                return ExceptionHandler.Handle<IEnumerable<DatabaseDto>>(e);
            }
        }

        private async Task<Database?> GetDatabase(int id, bool includeAddtionalData, CancellationToken cancellationToken)
        {
            Database? database = null;
            if (includeAddtionalData)
                database = await _context.Databases
                    .IncludeGroups()
                    .IncludeRecords()
                    .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
            else
                database = await _context.Databases
                    .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
            return database;
        }
    }
}
