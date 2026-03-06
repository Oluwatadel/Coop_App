using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopApplication.Domain.Entities
{
    public class Association : Auditable
    {
        public string Name { get; set; } = default!;
        public ICollection<User> Users { get; set; } = new List<User>();
        public Association(string name)
        {
            Name = name;
        }
    }
}
