using MediatR;
using Microsoft.EntityFrameworkCore;
using PasswordManager.Application.Dtos;
using PasswordManager.Application.Services;
using PasswordManager.Application.Services.Interfaces;
using PasswordManager.Data;
using PasswordManager.Domain.GroupAgregate;
using PasswordManager.SharedKernel.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Application.GroupAgregate.Queries
{
    public class GetGroupByIdQueryHandler : IRequestHandler<GetGroupByIdQuery, Result<GroupDto>>
    {
        private PasswordManagerDbContext _context;
        private IMapper<Group, GroupDto> _mapper;

        public GetGroupByIdQueryHandler(PasswordManagerDbContext context, IMapper<Group, GroupDto> mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<GroupDto>> Handle(GetGroupByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Group? group = null;
                if (request.IncludeAdditionalInformatoion)
                    group = await _context.Groups
                        .IncludeParentDatabase()
                        .IncludeRecords()
                        .FirstOrDefaultAsync(g => g.Id == request.Id, cancellationToken);
                else
                    group = await _context.Groups
                        .FirstOrDefaultAsync(g => g.Id == request.Id, cancellationToken);
                if (group == null)
                    return Result<GroupDto>.NotFound($"No such group with id: {request.Id}");
                return Result.Success(_mapper.Map(group));
            }
            catch (Exception e)
            {
                return ExceptionHandler.Handle<GroupDto>(e);
            }
        }
    }
}
