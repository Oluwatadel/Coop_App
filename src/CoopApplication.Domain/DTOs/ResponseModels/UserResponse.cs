namespace CoopApplication.Domain.DTOs.ResponseModels
{
    public record UserResponse
    {
        public Guid UserId { get; init; }
        public string AssociationName { get; init; }
        public string Role { get; init ; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }
        public string Phone { get; init; }
        public bool IsActive { get; init; }
    }
    
    public record UserDTO
    {
        public Guid UserId { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }
        public string Phone { get; init; }
        public bool IsActive { get; init; }
    }
}
