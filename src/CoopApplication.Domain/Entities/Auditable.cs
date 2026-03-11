namespace CoopApplication.Domain.Entities
{
    public abstract class Auditable
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedAt { get; set; }
        public Guid? Modifier { get; set; }
    }
}
