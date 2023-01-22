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

namespace PasswordManager.Application.GroupAgregate.Commands
{
    public class UpdateGroupsDataCommandHandler : IRequestHandler<UpdateGroupsDataCommand, Result<GroupDto>>
    {
        private PasswordManagerDbContext _context;
        private IMapper<Group, GroupDto> _mapper;

        public UpdateGroupsDataCommandHandler(PasswordManagerDbContext context, IMapper<Group, GroupDto> mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<GroupDto>> Handle(UpdateGroupsDataCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Group? group = await _context.Groups
                    .FirstOrDefaultAsync(g => g.Id == request.Group.Id, cancellationToken);
                if (group == null)
                    return Result<GroupDto>.NotFound($"No such group with id: {request.Group.Id}");
                if (group.Name != request.Group.Name)
                    group.ChangeName(request.Group.Name);
                _context.Groups.Update(group);
                await _context.SaveEntitiesAsync(cancellationToken);
                return Result.Success(_mapper.Map(group));
            }
            catch (Exception e)
            {
                return ExceptionHandler.Handle<GroupDto>(e);
            }
        }
    }
}
