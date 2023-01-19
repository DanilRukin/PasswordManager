using PasswordManager.Application.Dtos;
using PasswordManager.Application.Services.Interfaces;
using PasswordManager.Domain.DatabaseAgregate;
using PasswordManager.Domain.GroupAgregate;
using PasswordManager.Domain.RecordEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Application.Services
{
    internal class DatabaseToDatabaseDtoMapper : IMapper<Database, DatabaseDto>
    {
        public DatabaseDto Map(Database source)
        {
            DatabaseDto dto = new DatabaseDto
            {
                Name = source.Name,
                Id = source.Id,
            };
            Group[] groups = source.Groups.ToArray();
            if (groups != null)
                dto.GroupIds = groups.Select(g => g.Id);
            else
                dto.GroupIds = new List<int>();
            Record[] records = source.Records.ToArray();
            if (records != null)
                dto.RecordIds = records.Select(r => r.Id);
            else
                dto.RecordIds = new List<int>();
            return dto;
        }
    }
}
