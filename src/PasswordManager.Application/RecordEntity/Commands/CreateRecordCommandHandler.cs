using MediatR;
using Microsoft.EntityFrameworkCore;
using PasswordManager.Application.Dtos;
using PasswordManager.Application.Services;
using PasswordManager.Application.Services.Interfaces;
using PasswordManager.Data;
using PasswordManager.Domain.DatabaseAgregate;
using PasswordManager.Domain.GroupAgregate;
using PasswordManager.Domain.RecordEntity;
using PasswordManager.SharedKernel.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Application.RecordEntity.Commands
{
    public class CreateRecordCommandHandler : IRequestHandler<CreateRecordCommand, Result<RecordDto>>
    {
        private PasswordManagerDbContext _context;
        private IMapper<Record, RecordDto> _mapper;
        public async Task<Result<RecordDto>> Handle(CreateRecordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Record.RecordContainerId == null || request.Record.RecordContainerId == 0)
                    return Result<RecordDto>.Error("Can not to create record because it has no Group or Database");
                DateTime now = DateTime.UtcNow;
                Record record = _context.Create(request.Record.ResourceName, request.Record.ResourcePasswordHash, now,
                    request.Record.ResourceUrl, request.Record.Description);
                if (record == null)
                    return Result<RecordDto>.Error("Can not to create record");
                Group? group = await _context.Groups
                    .FirstOrDefaultAsync(g => g.Id == request.Record.RecordContainerId, cancellationToken);
                if (group != null)
                    group.AddRecord(record);
                else
                {
                    Database? database = await _context.Databases
                        .FirstOrDefaultAsync(d => d.Id == request.Record.RecordContainerId, cancellationToken);
                    if (database == null)
                        return Result<RecordDto>.NotFound($"No such group or database with id: {request.Record.RecordContainerId}");
                    else
                        database.AddRecord(record);
                }
                await _context.SaveEntitiesAsync(cancellationToken);
                return Result.Success(_mapper.Map(record));
            }
            catch (Exception e)
            {
                return ExceptionHandler.Handle<RecordDto>(e);
            }
        }
    }
}
