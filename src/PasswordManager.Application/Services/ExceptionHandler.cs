using PasswordManager.Domain.Exceptions;
using PasswordManager.SharedKernel.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Application.Services
{
    internal class ExceptionHandler
    {
        internal static Result Handle(Exception domainException)
        {
            Exception ex = domainException;
            List<string> errors = new List<string>();

            while (ex != null)
            {
                errors.Add(ex.Message);
                ex = ex.InnerException;
            }
            return Result.Error(errors.ToArray());
        }

        internal static Result<T> Handle<T>(Exception domainException)
        {
            Exception ex = domainException;
            List<string> errors = new List<string>();
            while (ex != null)
            {
                errors.Add(ex.Message);
                ex = ex.InnerException;
            }
            return Result<T>.Error(errors.ToArray());
        }
    }
}
