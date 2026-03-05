using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopApplication.Persistence.Repository.Interfaces
{
    public interface IUnitofWork
    {
        Task<int> SaveChanges(CancellationToken cancellationToken);
    }
}
