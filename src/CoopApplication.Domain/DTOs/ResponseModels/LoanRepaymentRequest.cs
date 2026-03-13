namespace CoopApplication.Domain.DTOs.ResponseModels;

public record RepaymentResponse(
    Guid Id,
    Guid LoanId,
    Guid TransactionId,
    decimal Amount,
    DateTime Date,
    DateTime CreatedAt,
    Guid CreatedBy
);