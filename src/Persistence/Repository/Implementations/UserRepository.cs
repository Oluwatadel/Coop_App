using CoopApplication.Domain.DTOs.RequestModels;
using CoopApplication.Domain.DTOs.ResponseModels;
using CoopApplication.Domain.Entities;
using CoopApplication.Domain.Enums;
using CoopApplication.Persistence.Context;
using CoopApplication.Persistence.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CoopApplication.Persistence.Repository.Implementations
{
    public class UserRepository(CoopDbContext context) : IUserRepository
    {
        public async Task<UserResponse> CreateUserAsync(User user, CancellationToken cancellationToken)
        {
            await context.Users.AddAsync(user, cancellationToken);
            var query = from u in context.Users
                        join association in context.Associations
                        on user.AssociationId equals association.Id
                        join role in context.Roles
                        on user.RoleId equals role.Id
                        select new { role, association };
            var result = await query.FirstOrDefaultAsync(u => u.association.Id == user.AssociationId && u.role.Id == user.RoleId, cancellationToken);
            return new UserResponse
            {
                UserId = user.Id,
                AssociationName = result!.association.Name,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.Phone,
                Role = result.role.Name
            };
        }

        public async Task<bool> ExistAsync(string email, string? phoneNumber, CancellationToken cancellationToken)
        {
            var query = context.Users.AsQueryable();
            if (!string.IsNullOrWhiteSpace(phoneNumber))
            {
                query = query.Where(u => u.Phone == phoneNumber);
            }
            if (!string.IsNullOrWhiteSpace(email))
            {
                query = query.Where(u => u.Email == email);
            }
            return await query
                .AnyAsync(u => u.Email == email, cancellationToken);
        }

        public async Task<IReadOnlyList<UserResponse>> GetAllUsersAsync(CancellationToken cancellationToken)
        {
            var query = from user in context.Users
                        join association in context.Associations
                        on user.AssociationId equals association.Id
                        join role in context.Roles
                        on user.RoleId equals role.Id
                        select new UserResponse
                        {
                            UserId = user.Id,
                            AssociationName = association.Name,
                            Email = user.Email,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Phone = user.Phone,
                            Role = role.Name
                        };
            return await query.OrderByDescending(u => u.LastName)
                .ToListAsync(cancellationToken);
        }

        public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var query = from user in context.Users
                        join association in context.Associations
                        on user.AssociationId equals association.Id
                        join role in context.Roles
                        on user.RoleId equals role.Id
                        select user;
            return await query.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        }

        public async Task<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            var query = from user in context.Users
                        join association in context.Associations
                        on user.AssociationId equals association.Id
                        join role in context.Roles
                        on user.RoleId equals role.Id
                        select user;
            return await query
                .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        }

        public UserResponse UpdateUser(User user)
        {
            context.Users.Update(user);
            var changes = context.SaveChanges();
            var association = context.Associations.FirstOrDefault(a => a.Id == user.AssociationId);
            var role = context.Roles.FirstOrDefault(r => r.Id == user.RoleId);
            return new UserResponse
            {
                UserId = user.Id,
                AssociationName = association.Name,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.Phone,
                Role = role.Name
            };
        }

        public async Task<IReadOnlyList<UserResponse?>> SearchUserAsync(SearchUser request, CancellationToken cancellationToken)
        {
            //To add pagination later
            var query = from user in context.Users
                        join association in context.Associations
                        on user.AssociationId equals association.Id
                        join role in context.Roles
                        on user.RoleId equals role.Id
                        select new { user, association, role };
            if (string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                query = query.Where(u =>
                    (EF.Functions.ILike(u.user.FirstName, request.SearchTerm)
                    || EF.Functions.ILike(u.user.LastName, request.SearchTerm)
                    || EF.Functions.ILike(u.user.Email, request.SearchTerm)
                    || EF.Functions.ILike(u.user.Phone, request.SearchTerm))
                    && u.user.IsActive == request.IsActive
                );
            }

            var data = await query.OrderByDescending(u => u.user.CreatedAt)
                .ToListAsync(cancellationToken);
            return [..data.Select(a => new UserResponse
            {
                UserId = a.user.Id,
                AssociationName = a.association.Name,
                Email = a.user.Email,
                FirstName = a.user.FirstName,
                LastName = a.user.LastName,
                Phone = a.user.Phone,
                Role = a.role.Name
            })];
        }

        public async Task<IReadOnlyList<UserResponse>> GetMembersOfAnAssociation(Guid associationId, CancellationToken cancellationToken)
        {
            //To add pagination later
            var query = from user in context.Users
                        join association in context.Associations
                        on user.AssociationId equals association.Id
                        join role in context.Roles
                        on user.RoleId equals role.Id
                        select new UserResponse
                        {
                            UserId = user.Id,
                            AssociationName = association.Name,
                            Email = user.Email,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Phone = user.Phone,
                            Role = role.Name
                        };
            var data = await query.OrderByDescending(u => u.LastName)
                .ToListAsync(cancellationToken);
            return data;
        }

        public async Task<UserDashBoardOverviewDto?> GetUserDashBoardOverviewDto(Guid userId, CancellationToken cancellationToken)
        {
            var query = from user in context.Users
                        join association in context.Associations
                        on user.AssociationId equals association.Id
                        join role in context.Roles
                        on user.RoleId equals role.Id
                        join loan in context.LoanTaken
                        on user.Id equals loan.UserId
                        join transaction in context.Transactions
                        on user.Id equals transaction.UserId
                        join account in context.Accounts
                        on user.Id equals account.UserId
                        where user.Id == userId
                        select new UserDashBoardOverviewDto
                        {
                            UserId = user.Id,
                            AssociationName = association.Name,
                            RoleId = role.Id,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Email = user.Email,
                            Phone = user.Phone,
                            LoansTakens = context.LoanTaken.Where(l => l.UserId == user.Id)
                                .Select(l => new LoanTakenDto
                                {
                                    UserId = l.UserId,
                                    LoanType = l.LoanType,
                                    PrincipalAmount = l.PrincipalAmount,
                                    TotalRepaymentAmount = l.TotalRepaymentAmount,
                                    MonthlyPaymentAmount = l.MonthlyPaymentAmount,
                                    BalanceRemaining = l.BalanceRemaining,
                                    Status = l.Status,
                                    StartDate = l.StartDate,
                                    EndDate = l.EndDate,
                                    LoanRepayments = context.LoanRepayments.Where(r => r.LoanId == l.Id).ToHashSet()
                                }).ToList(),
                            Transactions = context.Transactions.Where(t => t.UserId == user.Id)
                                .Select(t => new TransactionDto
                                {
                                    Id = t.Id,
                                    ReferenceNo = t.TransactionReferenceNo,
                                    Amount = t.Amount,
                                    TransactionType = t.TransactionType,
                                    PaymentMethod = t.PaymentMethod,
                                    Date = t.Date
                                }).ToList(),
                            Account = new AccountDto
                            {
                                TotalShares = account.TotalShares,
                                SavingsBalance = account.SavingsBalance,
                                TotalInterestAccrued = account.TotalInterestAccrued
                            }
                        };
            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<AdminDashBoardOverviewDto> GetAdminDashBoardOverviewDto(Guid userId, CancellationToken cancellationToken)
        {
            var query = from user in context.Users
                        join association in context.Associations
                        on user.AssociationId equals association.Id
                        join role in context.Roles
                        on user.RoleId equals role.Id
                        where association.Id == user.Id
                        select new { user, association, role };
            var data = await query.FirstOrDefaultAsync(u => u.user.Id == userId, cancellationToken);
            var userResponse = new UserResponse
            {
                AssociationName = data!.association.Name,
                Email = data.user.Email,
                FirstName = data.user.FirstName,
                LastName = data.user.LastName,
                Phone = data.user.Phone,
                Role = data.role.Name,
                UserId = userId,
                IsActive = data.user.IsActive
            };

            var members = from user in context.Users
                          join account in context.Accounts
                          on user.Id equals account.UserId
                          join loan in context.LoanTaken
                          on user.Id equals loan.UserId
                          where user.AssociationId == data.association.Id
                          select new { user, loan, account};

            var totalMembers = await members.CountAsync(cancellationToken);
            var TotalAmountSavedByMembers = await members.SumAsync(a => a.account.SavingsBalance, cancellationToken);
            var totalMembersShares = members.SumAsync(_ => _.account.TotalShares, cancellationToken);
            var totalLoanIssued = members.SumAsync(a => a.loan.PrincipalAmount, cancellationToken);
            var totalLoanRepaymentYetToBePaid = members.SumAsync(a => a.loan.BalanceRemaining, cancellationToken);
            var totalRepayments = members.SumAsync(a => a.loan.TotalRepaymentAmount - a.loan.BalanceRemaining, cancellationToken);
            var defaulters = m
        }
    }

    public record AdminDashBoardOverviewDto
    {
        public Guid AdminId { get; init; }
        public string FullName { get; init; }
        public Guid AssociationId { get; init; }
        public string AssociationName { get; init; }
        public int TotalMembers { get; init; }
        public decimal TotalShares { get; init; }
        public decimal TotalAmountSavedByMembers { get; init; }
        public decimal TotalAmountOfLoanIssued { get; init; }
        public decimal TotalAmountOfLoanRepaymentYetToBePaid { get; init; }
        public IReadOnlyList<Defaulter> Defaulters { get; init; } = [];
        public decimal TotalRepayments { get; init; }
    }

    public record Defaulter
    {         
        public Guid UserId { get; init; }
        public string FullName { get; init; }
        public string Email { get; init; }
        public string Phone { get; init; }
        public decimal AmountDue { get; init; }
    }

    public record UserDashBoardOverviewDto
    {
        public Guid UserId { get; set; }
        public string AssociationName { get; init; }
        public Guid RoleId { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }
        public string Phone { get; init; }
        public IReadOnlyList<LoanTakenDto> LoansTakens { get; init; } = [];
        public ICollection<TransactionDto> Transactions { get; init; } = [];
        public AccountDto Account { get; init; }

    }

    public record AccountDto
    {
        public decimal TotalShares { get; init; }
        public decimal SavingsBalance { get; init; }
        public decimal TotalInterestAccrued { get; init; }
    }

    public record TransactionDto
    {
        public string ReferenceNo { get; init; }
        public Guid Id { get; init; }
        public decimal Amount { get; init; }
        public TransactionType TransactionType { get; init; }
        public PaymentMethod PaymentMethod { get; init; }
        public DateTime Date { get; init; }
    }

    public record LoanTakenDto
    {
        public Guid UserId { get; init; }
        public LoanType LoanType { get; init; }
        public decimal PrincipalAmount { get; init; }
        public decimal TotalRepaymentAmount { get; init; }
        public decimal MonthlyPaymentAmount { get; init; }
        public decimal BalanceRemaining { get; init; } = 0;
        public LoanStatus Status { get; init; } = LoanStatus.Pending;
        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; init; }
        public HashSet<LoanRepayment> LoanRepayments { get; init; } = [];

    }

}
