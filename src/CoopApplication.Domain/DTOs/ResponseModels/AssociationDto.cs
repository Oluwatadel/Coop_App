namespace CoopApplication.Domain.DTOs.ResponseModels
{
    public record AssociationDto(Guid Id, string Name, string? Description, int MemberCount);
}
