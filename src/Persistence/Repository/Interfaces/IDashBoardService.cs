using CoopApplication.Domain.DTOs.ResponseModels;

namespace CoopApplication.Persistence.Repository.Interfaces
{
    public interface IDashBoardService
    {
        Task<UserDashBoardOverviewDto?> GetUserDashBoardOverview(Guid userId, CancellationToken cancellationToken);
        Task<ManagerDashBoardOverviewDto> GetManagerDashBoardOverview(Guid userId, CancellationToken cancellationToken);
        Task<AdminDashBoardOverviewDto> GetAdminDashBoardOverview(Guid userId, CancellationToken cancellationToken);
    }
}
