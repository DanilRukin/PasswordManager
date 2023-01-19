using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Application.Dtos
{
    public class GroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public IEnumerable<int> RecordIds { get; set; } = new List<int>();
        public int DatabaseId { get; set; } = 0;
    }
}
