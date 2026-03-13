namespace CoopApplication.Domain.DTOs.RequestModels;

public record RepaymentRequest(
    Guid? LoanId,
    Guid? UserId,
    Guid? TransactionId
);