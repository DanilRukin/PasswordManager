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
        public List<int> RecordIds { get; set; } = new List<int>();
        public List<int> GroupIds { get; set; } = new List<int>();
    }
}
