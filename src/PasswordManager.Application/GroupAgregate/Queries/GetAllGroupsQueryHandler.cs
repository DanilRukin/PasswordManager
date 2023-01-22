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
    public class GetAllGroupsQueryHandler : IRequestHandler<GetAllGroupsQuery, Result<IEnumerable<GroupDto>>>
    {
        private PasswordManagerDbContext _context;
        private IMapper<Group, GroupDto> _mapper;

        public GetAllGroupsQueryHandler(PasswordManagerDbContext context, IMapper<Group, GroupDto> mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<IEnumerable<GroupDto>>> Handle(GetAllGroupsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<Group> groups = new List<Group>();
                if (request.IncludeAdditionalData)
                    groups = await _context.Groups
                        .IncludeParentDatabase()
                        .IncludeRecords()
                        .ToListAsync(cancellationToken);
                else
                    groups = await _context.Groups
                        .ToListAsync(cancellationToken);
                return Result.Success(groups.Select(g => _mapper.Map(g)));
            }
            catch (Exception e)
            {
                return ExceptionHandler.Handle<Result<IEnumerable<GroupDto>>>(e);
            }
        }
    }
}
