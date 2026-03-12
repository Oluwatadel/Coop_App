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
        public async Task<User> CreateUserAsync(User user, CancellationToken cancellationToken)
        {
            await context.Users.AddAsync(user, cancellationToken);
            return user;
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

        public async Task<UserResponse?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var query = await (from user in context.Users
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
                            IsActive = user.IsActive,
                            Role = role.Name
                        }).FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
            return query;
        }

        public async Task<UserResponse?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            var query = await (from user in context.Users
                        join association in context.Associations
                        on user.AssociationId equals association.Id
                        join role in context.Roles
                        on user.RoleId equals role.Id
                        select new UserResponse
                        {
                            UserId = userId,
                            AssociationName = association.Name,
                            Email = user.Email,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Phone = user.Phone,
                            IsActive = user.IsActive,
                            Role = role.Name
                        }).FirstOrDefaultAsync(u => u.UserId == userId, cancellationToken);
            return query;
        }

        public async Task<User?> GetUserById(Guid userId, CancellationToken cancellationToken)
        {
            var query = from user in context.Users
                        join association in context.Associations
                        on user.AssociationId equals association.Id
                        join role in context.Roles
                        on user.RoleId equals role.Id
                        select user;
            var result = await query.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
            return result;
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
                        join role in context.Roles
                        on user.RoleId equals role.Id
                        where user.AssociationId == associationId
                        select new { user, role};
            var association = await context.Associations.FirstOrDefaultAsync(a => a.Id == associationId, cancellationToken);
            var data = await query
                .Where(a => a.user.AssociationId == associationId)
                .OrderByDescending(u => u.user.LastName)
                .Select(a => new UserResponse
                {
                    AssociationName = association.Name,
                    IsActive = a.user.IsActive,
                    Email = a.user.Email,
                    FirstName = a.user.FirstName,
                    LastName = a.user.LastName,
                    Phone = a.user.Phone,
                    Role = a.role.Name,
                    UserId = a.user.Id
                })
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
                        join account in context.Accounts
                            on user.Id equals account.UserId into accountGroup
                        from account in accountGroup.DefaultIfEmpty()
                        join loan in context.LoanTaken
                            on user.Id equals loan.UserId into loanGroup
                        from loan in loanGroup.DefaultIfEmpty()
                        where user.Id == userId
                        select new UserDashBoardOverviewDto
                        {
                            UserId = user.Id,
                            AssociationName = association.Name,
                            Role = role.Name,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Email = user.Email,
                            Phone = user.Phone,
                            Account = account == null ? null : new AccountDto
                            {
                                TotalShares = account.TotalShares,
                                SavingsBalance = account.SavingsBalance,
                                TotalInterestAccrued = account.TotalInterestAccrued
                            },
                            LoansTakens = loan == null
                                ? new List<LoanTakenDto>()
                                : new List<LoanTakenDto>
                                {
                                    new LoanTakenDto
                                    {
                                        UserId = loan.UserId,
                                        LoanType = loan.LoanType,
                                        PrincipalAmount = loan.PrincipalAmount,
                                        TotalRepaymentAmount = loan.TotalRepaymentAmount,
                                        MonthlyPaymentAmount = loan.MonthlyPaymentAmount,
                                        BalanceRemaining = loan.BalanceRemaining,
                                        Status = loan.Status,
                                        StartDate = loan.StartDate,
                                        EndDate = loan.EndDate,
                                        LoanRepayments = loan.LoanRepayments ?? new HashSet<LoanRepayment>()
                                    }
                                }
                        };

            var result = await query.FirstOrDefaultAsync(cancellationToken);
            return result;
        }

        public async Task<ManagerDashBoardOverviewDto> GetManagerDashBoardOverviewDto(Guid userId, CancellationToken cancellationToken)
        {
            var query = from user in context.Users
                        join association in context.Associations
                        on user.AssociationId equals association.Id
                        join role in context.Roles
                        on user.RoleId equals role.Id
                        where association.Id == user.AssociationId
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
            var totalAmountSavedByMembers = await members.SumAsync(a => a.account.SavingsBalance, cancellationToken);
            var totalMembersShares = await members.SumAsync(_ => _.account.TotalShares, cancellationToken);
            var totalLoanIssued = await members.SumAsync(a => a.loan.PrincipalAmount, cancellationToken);
            var totalLoanRepaymentYetToBePaid = await members.SumAsync(a => a.loan.BalanceRemaining, cancellationToken);
            var totalRepayments = await members.SumAsync(a => a.loan.TotalRepaymentAmount - a.loan.BalanceRemaining, cancellationToken);

            return new ManagerDashBoardOverviewDto
            {
                AdminId = userResponse.UserId,
                FullName = $"{userResponse.LastName} {userResponse.FirstName}",
                Role = "Manager",
                AssociationId = data.association.Id,
                AssociationName = data.association.Name,
                TotalMembers = totalMembers,
                TotalShares = totalMembersShares,
                TotalAmountSavedByMembers = totalAmountSavedByMembers,
                TotalAmountOfLoanIssued = totalLoanIssued,
                TotalAmountOfLoanRepaymentYetToBePaid = totalLoanRepaymentYetToBePaid,
                TotalRepayments = totalRepayments
            };
        }

        public async Task<AdminDashBoardOverviewDto> GetAdminDashBoardOverviewDto(Guid userId, CancellationToken cancellationToken)
        {
            var admin = await (
             from u in context.Users
             where u.Id == userId
             select new
             {
                 u.Id,
                 u.FirstName,
                 u.LastName
             })
             .FirstOrDefaultAsync(cancellationToken);

                var dashboardStats = await (
                    from dummy in context.Users.Take(1)
                    select new
                    {
                        TotalMembers = context.Users.Count(),
                        TotalShares = context.Accounts.Sum(a => (decimal?)a.TotalShares) ?? 0,
                        TotalSavings = context.Accounts.Sum(a => (decimal?)a.SavingsBalance) ?? 0,
                        TotalLoanIssued = context.LoanTaken.Sum(l => (decimal?)l.PrincipalAmount) ?? 0,
                        TotalLoanRemaining = context.LoanTaken.Sum(l => (decimal?)l.BalanceRemaining) ?? 0,
                        TotalRepayments = context.LoanTaken
                            .Sum(l => (decimal?)(l.TotalRepaymentAmount - l.BalanceRemaining)) ?? 0
                    })
                    .FirstAsync(cancellationToken);

                var associationDictionary = await (
                    from association in context.Associations
                    join user in context.Users
                        on association.Id equals user.AssociationId into members
                    select new
                    {
                        AssociationName = association.Name,
                        MemberCount = members.Count()
                    })
                    .ToDictionaryAsync(x => x.AssociationName, x => x.MemberCount, cancellationToken);

                return new AdminDashBoardOverviewDto
                {
                    AdminId = admin.Id,
                    FullName = $"Super Admin",
                    Role = "SuperAdmin",
                    AssociationAndTotalMembers = associationDictionary,
                    TotalMembers = dashboardStats.TotalMembers,
                    TotalShares = dashboardStats.TotalShares,
                    TotalAmountSavedByMembers = dashboardStats.TotalSavings,
                    TotalAmountOfLoanIssued = dashboardStats.TotalLoanIssued,
                    TotalAmountOfLoanRepaymentYetToBePaid = dashboardStats.TotalLoanRemaining,
                    TotalRepayments = dashboardStats.TotalRepayments
                };
            }

    }

    

}
