using CoopApplication.api.Exceptions;
using CoopApplication.Domain.DTOs.RequestModels;
using CoopApplication.Domain.DTOs.ResponseModels;
using CoopApplication.Domain.Entities;
using CoopApplication.Persistence.Repository.Interfaces;
using CoopApplication.Services.Interfaces;

namespace CoopApplication.Services.Implementations
{
    public class LoanTypeService(ILoanTypeRepository loanTypeRepository, IUnitofWork unitofWork) : ILoanTypeService
    {
        public async Task<LoanTypeResponse> CreateLoanTypeAsync(CreateLoanTypeRequest loanTypeRequest, CancellationToken cancellationToken)
        {
            var loanType = await loanTypeRepository.GetLoanTypeByNameAsync(loanTypeRequest.Name, cancellationToken);
            if(loanType != null)
            {
                throw new AlreadyExistsException($"Loan with name: {loanTypeRequest.Name} already exist");
            }
            var newLoanType = new LoanType(
                loanTypeRequest.Name, loanTypeRequest.Description,
                loanTypeRequest.MaximunLoanAmount, loanTypeRequest.minimumLoanAmount,
                loanTypeRequest.MinimumLoanRepayment, loanTypeRequest.AnnualInterestRate,
                loanTypeRequest.LiquidityPeriodInMonths);
            var returnedLoanType = await loanTypeRepository.CreateLoanTypeAsync(newLoanType, cancellationToken);
            var changes = await unitofWork.SaveChanges(cancellationToken);
            if(changes <= 0)
            {
                throw new SaveOperationException($"Error saving loanType");
            }
            return new LoanTypeResponse(
                returnedLoanType.Id,
                returnedLoanType.Name,
                returnedLoanType.Description,
                returnedLoanType.MinimumLoanRepayment,
                returnedLoanType.AnnualInterestRate,
                returnedLoanType.LiquidityPeriodInMonths,
                returnedLoanType.LoanVersion);

        }

        public async Task<IReadOnlyList<LoanTypeResponse>> GetAllLoanTpesAsync(CancellationToken cancellationToken)
        {
            var returnedLoanTypes = await loanTypeRepository.GetAllLoanTypesAsync(cancellationToken);
            return [..returnedLoanTypes.Select(a => new LoanTypeResponse(
                a.Id,
                a.Name,
                a.Description,
                a.MinimumLoanRepayment,
                a.AnnualInterestRate,
                a.LiquidityPeriodInMonths,
                a.LoanVersion))];
        }

        public async Task<LoanTypeResponse> GetLoanTypeAsync(Guid loanTypeId, CancellationToken cancellationToken)
        {
            var loanType = await loanTypeRepository.GetLoanTypeByIdAsync(loanTypeId, cancellationToken);
            if(loanType == null)
            {
                throw new NotFoundException($"Loan type with Id: {loanTypeId} is not found!!!");
            }
            return new LoanTypeResponse(
                loanType.Id,
                loanType.Name,
                loanType.Description,
                loanType.MinimumLoanRepayment,
                loanType.AnnualInterestRate,
                loanType.LiquidityPeriodInMonths,
                loanType.LoanVersion);
        }

        public async Task<LoanTypeResponse> GetLoanTypeByNameAsync(string loanName, CancellationToken cancellationToken)
        {
            var loanType = await loanTypeRepository.GetLoanTypeByNameAsync(loanName, cancellationToken);
            if (loanType == null)
            {
                throw new NotFoundException($"Loan type with name: {loanName} is not found!!!");
            }
            return new LoanTypeResponse(
                loanType.Id,
                loanType.Name,
                loanType.Description,
                loanType.MinimumLoanRepayment,
                loanType.AnnualInterestRate,
                loanType.LiquidityPeriodInMonths,
                loanType.LoanVersion);
        }

        public async Task<LoanTypeResponse> UpdateTypeAsync(Guid loanTypeId, UpdateLoanTypeRequest loanTypeRequest, CancellationToken cancellationToken)
        {
            var loanType = await loanTypeRepository.GetLoanTypeByIdAsync(loanTypeId, cancellationToken);
            if (loanType == null)
            {
                throw new NotFoundException($"Loan type with Id: {loanTypeId} is not found!!!");
            }
            var newLoan = loanType.CreateNewVersionOfLoanType(
                loanTypeRequest.Description,
                loanType,
                loanTypeRequest.Name,
                loanTypeRequest.MinimumLoanRepayment, loanTypeRequest.AnnualInterestRate,
                loanTypeRequest.LiquidityPeriodInMonths,
                loanTypeRequest.MaximunLoanAmount, loanTypeRequest.MinimumLoanAmount);
            newLoan.AddOldVersionofLoanType(loanType);
            loanTypeRepository.UpdateLoanType(newLoan);
            var changes = await unitofWork.SaveChanges(cancellationToken);
            if(changes <= 0)
            {
                throw new SaveOperationException("Problem saving loan type");
            }
            return new LoanTypeResponse(
                newLoan.Id,
                newLoan.Name,
                newLoan.Description,
                newLoan.MinimumLoanRepayment,
                newLoan.AnnualInterestRate,
                newLoan.LiquidityPeriodInMonths,
                loanType.LoanVersion);
        }
    }
}
