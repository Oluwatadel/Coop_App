namespace CoopApplication.Domain.DTOs.RequestModels;

public record LoanRepaymentRequest(
    Guid? LoanId,
    Guid? UserId,
    Guid? TransactionId
);