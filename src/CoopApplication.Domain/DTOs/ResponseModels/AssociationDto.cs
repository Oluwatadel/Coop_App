namespace CoopApplication.Domain.DTOs.ResponseModels
{
    //public record AssociationDto(
    //    Guid Id, string Name, 
    //    string? Description, int MemberCount
    // );

    public record AssociationDto(Guid Id, string Name, string Description, int TotalMembers, AssociationLoanSummary AssociationSummary);

    public record AssociationLoanSummary(int loanCount,
        decimal TotalPrincipal, decimal TotalBalance, decimal TotalSavings);
}
