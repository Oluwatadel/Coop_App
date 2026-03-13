namespace CoopApplication.Domain.DTOs.ResponseModels;

public record LoanRepaymentResponse(
    Guid Id,
    Guid LoanId,
    Guid TransactionId,
    decimal Amount,
    DateTime Date,
    DateTime CreatedAt,
    Guid CreatedBy
);