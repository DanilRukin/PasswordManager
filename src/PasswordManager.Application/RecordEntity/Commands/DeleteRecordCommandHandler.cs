using MediatR;
using Microsoft.EntityFrameworkCore;
using PasswordManager.Application.Services;
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
    public class DeleteRecordCommandHandler : IRequestHandler<DeleteRecordCommand, Result>
    {
        private PasswordManagerDbContext _context;

        public DeleteRecordCommandHandler(PasswordManagerDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Result> Handle(DeleteRecordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Record? toDelete = await _context.Records
                    .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);
                if (toDelete == null)
                    return Result.NotFound($"No such record with id: {request.Id}");
                _context.Records.Remove(toDelete);
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
