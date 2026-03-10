using CoopApplication.Domain.Enums;

namespace CoopApplication.Domain.DTOs.RequestModels
{
    public record CreateRoleRequest(string Name);
    public record UpdateRoleRequest(string Name);
    public record UserRequest(Guid AssociationId, string Firstname, string Lastname, string Email, string PhoneNumber);
    public record UpdateUserRequest(string Firstname, string Lastname, string Email, string PhoneNumber);
    public record LoginRequest(string Email, string PhoneNumber);
    public record SearchUser(string SearchTerm, bool IsActive = true);
    public record CreateAssociationRequest(string AssociationName, string Description);

    public record TransactionFilterDto
    {
        public string? FullName { get; init; }
        public string? AssociationName { get; init; }
        public DateTime? Date { get; init; }
        public decimal? Amount { get; init; }
        public TransactionType? TransactionType { get; init; }
        public PaymentMethod? PaymentMethod { get; init; }
    }

}
