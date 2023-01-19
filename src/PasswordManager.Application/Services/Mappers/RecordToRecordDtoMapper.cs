using PasswordManager.Application.Dtos;
using PasswordManager.Application.Services.Interfaces;
using PasswordManager.Domain.RecordEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Application.Services.Mappers
{
    internal class RecordToRecordDtoMapper : IMapper<Record, RecordDto>
    {
        public RecordDto Map(Record source)
        {
            DateTime now = DateTime.UtcNow;
            RecordDto dto = new RecordDto(now, now, now);
            if (source != null)
            {
                dto.ResourceName = source.ResourceName;
                dto.ResourceUrl = source.ResourceUrl;
                dto.Description = source.Description;
                dto.CreationDate = source.CreationDate;
                dto.LastAccessDate = source.LastAccessDate;
                dto.LastModifiedDate = source.LastModifiedDate;
                dto.RecordContainerId = source.RecordContainerId ?? 0;
            }
            return dto;
        }
    }
}
