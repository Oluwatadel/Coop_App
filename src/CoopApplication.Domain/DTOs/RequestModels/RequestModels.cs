using CoopApplication.Domain.Enums;
using System;

namespace CoopApplication.Domain.DTOs.RequestModels
{
    public record CreateRoleRequest(string Name);
    public record UpdateRoleRequest(string Name);
    public record UserRequest(Guid AssociationId, string Firstname, string Lastname, string Email, string PhoneNumber);
    public record UpdateUserRequest(string Firstname, string Lastname, string Email, string PhoneNumber);
    public record LoginRequest(string Email, string PhoneNumber);
    public record SearchUser(string SearchTerm, bool IsActive = true);
    public record CreateAssociationRequest(string AssociationName, string Description);


    public record CreateLoanTypeRequest(string Name, string Description, decimal MinimumLoanRepayment,
        decimal AnnualInterestRate, int LiquidityPeriodInMonths,
        decimal MaximunLoanAmount, decimal minimumLoanAmount);

    public record UpdateLoanTypeRequest(string Name, string? Description, 
        decimal? MinimumLoanRepayment,
        decimal? AnnualInterestRate, int? LiquidityPeriodInMonths,
        decimal? MaximunLoanAmount, decimal? MinimumLoanAmount);

}
