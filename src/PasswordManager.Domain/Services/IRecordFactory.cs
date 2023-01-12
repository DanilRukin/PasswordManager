using PasswordManager.Domain.RecordEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Domain.Services
{
    public interface IRecordFactory
    {
        Record Create(string resourceName, string resourcePasswordHash, DateTime creationDate,
                        string resourceUrl = "", string description = "");
    }
}
