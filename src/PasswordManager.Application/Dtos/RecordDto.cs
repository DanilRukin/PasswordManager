using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Application.Dtos
{
    public class RecordDto
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public DateTime LastAccessDate { get; set; }
        public string Description { get; set; } = "";
        public string ResourceName { get; set; } = "";
        public string ResourcePasswordHash { get; set; } = "";
        public string ResourceUrl { get; set; } = "";

        public int? RecordContainerId { get; set; } = 0;

        public RecordDto(DateTime creationDate, DateTime lastAccessDate, DateTime lastModifiedDate)
        {
            CreationDate = creationDate;
            LastAccessDate = lastAccessDate;
            LastModifiedDate = lastModifiedDate;
        }
    }
}
