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

        public string? Description { get; set; } = default!;

        public Association(string name, string? description)
        {
            Name = name;
            Description = description;
        }
    }
}
