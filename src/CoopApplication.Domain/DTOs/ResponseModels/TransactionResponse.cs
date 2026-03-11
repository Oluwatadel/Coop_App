using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoopApplication.Domain.Enums;

namespace CoopApplication.Domain.DTOs.ResponseModels
{
    public record TransactionResponse
    {
        public string ReferenceNo { get; set; } 
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public TransactionType TransactionType { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public DateTime Date { get; set; }
    }

namespace CoopApplication.Domain.DTOs.ResponseModels
{
    public record LoanTypeResponse(Guid Id, string Name, string Description, decimal MinimumLoanRepayment,
        decimal AnnualInterestRate, int LiquidityPeriodInMonths, int LoanVersion);
}
