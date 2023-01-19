using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Application.Dtos
{
    public class DatabaseDto
    {
        public string Name { get; set; } = "";
        public int Id { get; set; }
        public IEnumerable<int> RecordIds { get; set; } = new List<int>();
        public IEnumerable<int> GroupIds { get; set; } = new List<int>();
    }
}
