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
    public class GetRangeOfRecordsQueryHandler : IRequestHandler<GetRangeOfRecordsQuery, Result<IEnumerable<RecordDto>>>
    {
        private PasswordManagerDbContext _context;
        private IMapper<Record, RecordDto> _mapper;

        public GetRangeOfRecordsQueryHandler(PasswordManagerDbContext context, IMapper<Record, RecordDto> mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<IEnumerable<RecordDto>>> Handle(GetRangeOfRecordsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                ICollection<Record> records = new List<Record>();
                Record? record;
                foreach (int id in request.Ids)
                {
                    record = await GetRecord(id, request.IncludeAdditionalData, cancellationToken);
                    if (record == null)
                        return Result<IEnumerable<RecordDto>>.NotFound($"No such record with id: {id}");
                    else
                        records.Add(record);
                }
                return Result.Success(records.Select(r => _mapper.Map(r)));   
            }
            catch (Exception e)
            {
                return ExceptionHandler.Handle<IEnumerable<RecordDto>>(e);
            }
        }

        private async Task<Record?> GetRecord(int id, bool includeAdditionalData, CancellationToken cancellationToken)
        {
            Record? record = null;
            if (includeAdditionalData)
                record = await _context.Records
                    .IncludeContainer()
                    .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
            else
                record = await _context.Records
                    .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
            return record;
        }
    }
}
