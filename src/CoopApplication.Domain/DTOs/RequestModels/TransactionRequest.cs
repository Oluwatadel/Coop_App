using System.ComponentModel.DataAnnotations;
using CoopApplication.Domain.Enums;

namespace CoopApplication.Domain.DTOs.RequestModels
{
    public record TransactionRequestDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Amount must be greater than zero")]
        public decimal Amount { get; set; }

        [Required]
        public TransactionType TransactionType { get; set; }

        [Required]
        public PaymentMethod PaymentMethod { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }

}
