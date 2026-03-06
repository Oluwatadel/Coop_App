using System.Security.Principal;

namespace CoopApplication.Domain.Entities
{
    public class User : Auditable
    {
        public Guid AssociationId { get; set; }
        public Guid RoleId { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public bool IsActive { get; set; }
        public Association Association { get; set; } = default!;
        public Account Account { get; set; } = default!;
        public Role Role { get; set; } = default!;
        public ICollection<LoanTaken> LoansTaken { get; set; } = new List<LoanTaken>();
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    }
}
