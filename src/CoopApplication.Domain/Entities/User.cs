using CoopApplication.Domain.DTOs.RequestModels;
using System.Security.Principal;

namespace CoopApplication.Domain.Entities
{
    public class User : Auditable
    {
        //public string AssociationNumber { get; set; } = default!;
        public Guid AssociationId { get; set; }
        public Guid RoleId { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public bool IsActive { get; set; } = true;
        public ICollection<Guid> LoansTakenIds { get; set; } = [];
        public ICollection<Guid> TransactionIds { get; set; } = [];

        public User() { }

        public User(Guid associationId, Guid roleId, string firstName, string lastName, string email, string phone)
        {
            AssociationId = associationId;
            RoleId = roleId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
        }
        public void AddLoanTaken(Guid loanTakenId)
        {
            LoansTakenIds.Add(loanTakenId);
        }

        public void AddTransactionId(Guid TransactionIs)
        {
            TransactionIds.Add(TransactionIs);
        }

        public void UpdateRole(Guid roleId)
        {
            RoleId = roleId;
        }

        public void UpdateName(string firstName, string lastName)
        {
            LastName ??= lastName;
            FirstName ??= firstName;
        }

        public void updateEmail(string email)
        {
            Email ??= email;
        }
        
        public void updatePhonenumber(string phoneNumber)
        {
            Phone ??= phoneNumber;
        }

        public void Update(UpdateUserRequest request)
        {
            UpdateName(request.Firstname, request.Lastname);
            updateEmail(request.Email);
            updatePhonenumber(request.PhoneNumber);
        }

         public void DeactivateUser()
        {
            IsActive = false;
        }
         public void ActivateUser()
        {
            IsActive = true;
        }

    }
}
