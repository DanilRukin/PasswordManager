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

namespace PasswordManager.Application.RecordEntity.Queries
{
    public class GetRecordByIdQueryHandler : IRequestHandler<GetRecordByIdQuery, Result<RecordDto>>
    {
        private PasswordManagerDbContext _context;
        private IMapper<Record, RecordDto> _mapper;

        public GetRecordByIdQueryHandler(PasswordManagerDbContext context, IMapper<Record, RecordDto> mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<RecordDto>> Handle(GetRecordByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Record? record;
                if (request.IncludeAdditionalData)
                    record = await _context.Records
                       .IncludeContainer()
                       .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);
                else
                    record = await _context.Records
                        .FirstOrDefaultAsync(r => r.Id == request.Id);
                if (record == null)
                    return Result<RecordDto>.NotFound($"No such record with id: {request.Id}");
                return Result.Success(_mapper.Map(record));
            }
            catch (Exception e)
            {
                return ExceptionHandler.Handle<RecordDto>(e);
            }
        }
    }
}
