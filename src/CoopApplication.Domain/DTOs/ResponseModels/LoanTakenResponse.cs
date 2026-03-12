using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoopApplication.Domain.Enums;

namespace CoopApplication.Domain.DTOs.ResponseModels
{
    public record LoanTakenResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string LoanTypeName { get; set; }
        public decimal PrincipalAmount { get; set; }
        public decimal TotalRepaymentAmount { get; set; }
        public decimal MonthlyPaymentAmount { get; set; }
        public decimal BalanceRemaining { get; set; }
        public LoanStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
