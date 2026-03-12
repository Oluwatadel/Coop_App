using CoopApplication.Domain.DTOs.ResponseModels;
using CoopApplication.Persistence.Repository.Interfaces;

namespace CoopApplication.Persistence.Repository.Implementations
{
    public class DashBoardService(IUserRepository userRepository) : IDashBoardService
    {
        public async Task<AdminDashBoardOverviewDto> GetAdminDashBoardOverview(Guid userId, CancellationToken cancellationToken)
        {
            var userDashboardDetails = await userRepository.GetAdminDashBoardOverviewDto(userId, cancellationToken);
            return userDashboardDetails;
        }

        public async Task<ManagerDashBoardOverviewDto> GetManagerDashBoardOverview(Guid userId, CancellationToken cancellationToken)
        {
            var userDashboardDetails = await userRepository.GetManagerDashBoardOverviewDto(userId, cancellationToken);
            return userDashboardDetails;
        }

        public async Task<UserDashBoardOverviewDto?> GetUserDashBoardOverview(Guid userId, CancellationToken cancellationToken)
        {
            var userDashboardDetails = await userRepository.GetUserDashBoardOverviewDto(userId, cancellationToken);
            return userDashboardDetails;
        }
    }
}
