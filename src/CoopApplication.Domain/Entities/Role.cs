using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopApplication.Domain.Entities
{
    public class Role : Auditable
    {
        public string Name { get; set; } = default!;

        public Role(string name)
        {
            Name = name;
        }
    }
}
