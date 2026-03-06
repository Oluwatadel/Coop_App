using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoopApplication.Domain.Enums;

namespace CoopApplication.Domain.Entities
{
    public class LoanTaken : Auditable
    {
        public Guid UserId { get; set; }
        public Guid LoanTypeId { get; set; }
        public decimal PrincipalAmount { get; set; }
        public decimal TotalPayable { get; set; }
        public decimal BalanceRemaining { get; set; }
        public LoanStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public LoanType LoanType { get; set; } = default!;
        public User User { get; set; } = default!;

    }
}
