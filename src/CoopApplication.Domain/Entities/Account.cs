using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopApplication.Domain.Entities
{
    public class Account : Auditable
    {
        public Guid UserId { get; set; }
        public int TotalShares { get; set; }
        public decimal SavingsBalance { get; set; }
        public decimal TotalInterestAccrued { get; set; }
        public User User { get; set; } = default!;
    }
}
