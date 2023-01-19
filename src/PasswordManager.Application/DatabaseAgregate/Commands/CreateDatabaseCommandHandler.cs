using MediatR;
using Microsoft.EntityFrameworkCore;
using PasswordManager.Application.Dtos;
using PasswordManager.Application.Services;
using PasswordManager.Application.Services.Interfaces;
using PasswordManager.Data;
using PasswordManager.Domain.DatabaseAgregate;
using PasswordManager.Domain.Exceptions;
using PasswordManager.Domain.GroupAgregate;
using PasswordManager.Domain.RecordEntity;
using PasswordManager.Domain.Services;
using PasswordManager.SharedKernel.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Application.DatabaseAgregate.Commands
{
    public class CreateDatabaseCommandHandler : IRequestHandler<CreateDatabaseCommand, Result<DatabaseDto>>
    {
        private PasswordManagerDbContext _context;
        private IMapper<Database, DatabaseDto> _mapper;

        public CreateDatabaseCommandHandler(PasswordManagerDbContext context, IMapper<Database, DatabaseDto> mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<DatabaseDto>> Handle(CreateDatabaseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Database database = ((IDatabaseFactory)_context).Create(request.Database.Name);
                if (request.Database.GroupIds != null && request.Database.GroupIds.Any())
                {
                    Group? group;
                    foreach (int id in request.Database.GroupIds)
                    {
                        group = await _context.Groups
                            .IncludeParentDatabase()
                            .IncludeRecords()
                            .FirstOrDefaultAsync(g => g.Id == id, cancellationToken);
                        if (group == null)
                            return Result<DatabaseDto>.NotFound($"No such group with id: {id}");
                        database.AddGroup(group);
                    }
                }
                if (request.Database.RecordIds != null && request.Database.RecordIds.Any())
                {
                    Record? record;
                    foreach (int id in request.Database.RecordIds)
                    {
                        record = await _context.Records
                            .IncludeContainer()
                            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
                        if (record == null)
                            return Result<DatabaseDto>.NotFound($"No such record with id: {id}");
                        database.AddRecord(record);
                    }
                }
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
