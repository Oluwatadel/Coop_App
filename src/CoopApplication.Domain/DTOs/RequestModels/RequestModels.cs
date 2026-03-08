namespace CoopApplication.Domain.DTOs.RequestModels
{
    public record CreateRoleRequest(string Name);
    public record UpdateRoleRequest(string Name);
    public record UserRequest(Guid AssociationId, string Firstname, string Lastname, string Email, string PhoneNumber);
    public record UpdateUserRequest(string Firstname, string Lastname, string Email, string PhoneNumber);
    public record LoginRequest(string Email, string PhoneNumber);
    public record SearchUser(string SearchTerm, bool IsActive = true);

}
