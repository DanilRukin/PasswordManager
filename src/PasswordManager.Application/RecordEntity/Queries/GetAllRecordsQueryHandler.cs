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
    public class GetAllRecordsQueryHandler : IRequestHandler<GetAllRecordsQuery, Result<IEnumerable<RecordDto>>>
    {
        private PasswordManagerDbContext _context;
        private IMapper<Record, RecordDto> _mapper;

        public GetAllRecordsQueryHandler(PasswordManagerDbContext context, IMapper<Record, RecordDto> mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<IEnumerable<RecordDto>>> Handle(GetAllRecordsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<Record> records;
                if (request.IncludeAdditionalData)
                    records = await _context.Records
                        .IncludeContainer()
                        .ToListAsync(cancellationToken);
                else
                    records = await _context.Records.ToListAsync(cancellationToken);
                if (records == null || !records.Any())
                    return Result<IEnumerable<RecordDto>>.NotFound("No records in storage");
                else
                    return Result.Success(records.Select(r => _mapper.Map(r)));
            }
            catch (Exception e)
            {
                return ExceptionHandler.Handle<IEnumerable<RecordDto>>(e);
            }
        }
    }
}
