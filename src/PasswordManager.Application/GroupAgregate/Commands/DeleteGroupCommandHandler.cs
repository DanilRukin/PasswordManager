using MediatR;
using Microsoft.EntityFrameworkCore;
using PasswordManager.Application.Services;
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
    public class DeleteGroupCommandHandler : IRequestHandler<DeleteGroupCommand, Result>
    {
        private PasswordManagerDbContext _context;

        public DeleteGroupCommandHandler(PasswordManagerDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Result> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Group? group = await _context.Groups
                    .FirstOrDefaultAsync(g => g.Id == request.Id, cancellationToken);
                if (group == null)
                    return Result.NotFound($"No group with specified id: {request.Id}");
                _context.Groups.Remove(group);
                await _context.SaveChangesAsync(cancellationToken);
                return Result.Success();
            }
            catch (Exception e)
            {
                return ExceptionHandler.Handle(e);
            }
        }
    }
}
