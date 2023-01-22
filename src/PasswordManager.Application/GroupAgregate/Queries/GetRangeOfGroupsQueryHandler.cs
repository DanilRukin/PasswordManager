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
    public class GetRangeOfGroupsQueryHandler : IRequestHandler<GetRangeOfGroupsQuery, Result<IEnumerable<GroupDto>>>
    {
        private PasswordManagerDbContext _context;
        private IMapper<Group, GroupDto> _mapper;

        public GetRangeOfGroupsQueryHandler(PasswordManagerDbContext context, IMapper<Group, GroupDto> mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<IEnumerable<GroupDto>>> Handle(GetRangeOfGroupsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<Group> groups = new List<Group>();
                Group? group;
                foreach (int id in request.Ids)
                {
                    group = await GetGroup(id, request.IncludeAdditionalData, cancellationToken);
                    if (group == null)
                        return Result<IEnumerable<GroupDto>>.NotFound($"No such group with id: {id}");
                    groups.Add(group);
                }
                return Result.Success(groups.Select(g => _mapper.Map(g)));
            }
            catch (Exception e)
            {
                return ExceptionHandler.Handle<Result<IEnumerable<GroupDto>>>(e);
            }
        }

        private async Task<Group?> GetGroup(int id, bool includeAdditionalData, CancellationToken cancellationToken)
        {
            Group? group = null;
            if (includeAdditionalData)
                group = await _context.Groups
                    .IncludeParentDatabase()
                    .IncludeRecords()
                    .FirstOrDefaultAsync(g => g.Id == id, cancellationToken);
            else
                group = await _context.Groups
                    .FirstOrDefaultAsync(cancellationToken);
            return group;
        }
    }
}
