using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoopApplication.Domain.Enums;

namespace CoopApplication.Domain.Entities
{
    public class Transaction : Auditable
    {
        public Guid UserId { get; set; }

        public Guid? LoanId { get; set; }

        public decimal Amount { get; set; }

        public TransactionType TransactionType { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public Guid AdminId { get; set; }

        public DateTime Date { get; set; }

        public User User { get; set; } = default!;

        public LoanTaken? Loan { get; set; }
    }
}
