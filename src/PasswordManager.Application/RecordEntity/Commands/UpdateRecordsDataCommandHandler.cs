using MediatR;
using Microsoft.EntityFrameworkCore;
using PasswordManager.Application.Dtos;
using PasswordManager.Application.Services;
using PasswordManager.Application.Services.Interfaces;
using PasswordManager.Data;
using PasswordManager.Domain.RecordEntity;
using PasswordManager.SharedKernel.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Application.RecordEntity.Commands
{
    public class UpdateRecordsDataCommandHandler : IRequestHandler<UpdateRecordsDataCommand, Result<RecordDto>>
    {
        private PasswordManagerDbContext _context;
        private IMapper<Record, RecordDto> _mapper;

        public UpdateRecordsDataCommandHandler(PasswordManagerDbContext context, IMapper<Record, RecordDto> mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<RecordDto>> Handle(UpdateRecordsDataCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Record? record = await _context.Records
                    .FirstOrDefaultAsync(r => r.Id == request.Record.Id, cancellationToken);
                if (record == null)
                    return Result<RecordDto>.NotFound($"No such record with id: {request.Record.Id}");

                if (request.Record.UserName != record.UserName)
                    record.ChangeUserName(request.Record.UserName);
                if (request.Record.ResourceName != record.ResourceName)
                    record.ChangeResourceName(request.Record.ResourceName);
                if (request.Record.ResourcePasswordHash != record.ResourcePasswordHash)
                    record.ChangeResourcePasswordHash(request.Record.ResourcePasswordHash);
                if (request.Record.ResourceUrl != record.ResourceUrl)
                    record.ChangeResourceUrl(request.Record.ResourceUrl);
                if (request.Record.Description != record.Description)
                    record.ChangeDescription(request.Record.Description);

                if (request.Record.LastAccessDate != record.LastAccessDate)
                    record.ChangeLastAccessDate(request.Record.LastAccessDate);
                if (request.Record.LastModifiedDate != record.LastModifiedDate)
                    record.ChangeLastModifiedDate(request.Record.LastModifiedDate);

                _context.Records.Update(record);
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
