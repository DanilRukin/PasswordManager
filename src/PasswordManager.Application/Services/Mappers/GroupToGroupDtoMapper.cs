using PasswordManager.Application.Dtos;
using PasswordManager.Application.Services.Interfaces;
using PasswordManager.Domain.GroupAgregate;
using PasswordManager.Domain.RecordEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Application.Services.Mappers
{
    internal class GroupToGroupDtoMapper : IMapper<Group, GroupDto>
    {
        public GroupDto Map(Group source)
        {
            if (source == null)
                return new GroupDto();
            GroupDto dto = new GroupDto
            {
                Name = source.Name,
                Id = source.Id,
            };
            if (source.ParentDatabase != null)
                dto.DatabaseId = source.ParentDatabase.Id;
            else
                dto.DatabaseId = 0;
            Record[] records = source.Records.ToArray();
            if (records != null)
                dto.RecordIds = records.Select(r => r.Id);
            else
                dto.RecordIds = new List<int>();
            return dto;
        }
    }
}
