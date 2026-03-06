using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopApplication.Domain.Entities
{
    public class LoanType : Auditable
    {
        public string Name { get; set; } = default!;
        public int MinimumLoanRepayment { get; set; }
        public decimal AnnualInterestRate { get; set; }
        public int LiquidityPeriod { get; set; }
        public ICollection<LoanTaken> Loans { get; set; } = new List<LoanTaken>();
    }
}
