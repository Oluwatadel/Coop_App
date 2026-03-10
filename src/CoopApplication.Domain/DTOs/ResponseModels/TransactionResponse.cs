using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopApplication.Domain.DTOs.ResponseModels
{
    public record LoanTypeResponse(string Name, string Description, decimal MinimumLoanRepayment,
        decimal AnnualInterestRate, int LiquidityPeriodInMonths, int LoanVersion);
}
