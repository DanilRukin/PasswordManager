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

namespace PasswordManager.Application.GroupAgregate.Commands
{
    public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, Result<GroupDto>>
    {
        private PasswordManagerDbContext _context;
        private IMapper<Group, GroupDto> _mapper;

        public CreateGroupCommandHandler(PasswordManagerDbContext context, IMapper<Group, GroupDto> mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<GroupDto>> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Group.DatabaseId < 1)
                    return Result<GroupDto>.Error($"Enable to create group because no parent database defined");
                Group group = _context.Create(request.Group.Name);
                Database? parentDatabase = await _context.Databases
                    .FirstOrDefaultAsync(d => d.Id == request.Group.DatabaseId, cancellationToken);
                if (parentDatabase == null)
                    return Result<GroupDto>.NotFound($"No database with specified id: {request.Group.DatabaseId} in storage");
                parentDatabase.AddGroup(group);
                if (request.Group.RecordIds != null && request.Group.RecordIds.Any())
                {
                    Record? record;
                    foreach (int id in request.Group.RecordIds)
                    {
                        record = await _context.Records
                            .IncludeContainer()
                            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
                        if (record == null)
                            return Result<GroupDto>.NotFound($"No such record with id: {id}");
                        group.AddRecord(record);
                    }
                }
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
