using MediatR;
using Microsoft.EntityFrameworkCore;
using PasswordManager.Application.Services;
using PasswordManager.Data;
using PasswordManager.Domain.DatabaseAgregate;
using PasswordManager.SharedKernel.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Application.DatabaseAgregate.Commands
{
    public class DeleteDatabaseCommandHandler : IRequestHandler<DeleteDatabaseCommand, Result>
    {
        private PasswordManagerDbContext _context;

        public DeleteDatabaseCommandHandler(PasswordManagerDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Result> Handle(DeleteDatabaseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Database? database = await _context.Databases
                    .FirstOrDefaultAsync(d => d.Id == request.DatabaseId, cancellationToken);
                if (database == null)
                    return Result.NotFound($"No such database with id: {request.DatabaseId}");
                else
                {
                    _context.Databases.Remove(database);
                    _context.SaveChanges();
                    return Result.Success();
                }
            }
            catch (Exception e)
            {
                return ExceptionHandler.Handle(e);
            }
        }
    }
}
