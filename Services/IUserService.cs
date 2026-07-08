using TitleDeedManagementSystem.Models;

namespace TitleDeedManagementSystem.Services
{
  public interface IUserService
  {
    Task<IEnumerable<User>> GetAllUsersAsync();

    Task<User?> GetUserByIdAsync(int id);

    Task AddUserAsync(User user);
    Task AddUserRoleAsync(UserRole userRole);

    Task UpdateUserAsync(User user);
    Task UpdateUserRoleAsync(int userId, List<int> roleIds);

    Task DeleteUserAsync(int id);
    Task<bool> EmployeeIdExistsAsync(string employeeId);
    Task<bool> UserNameExistsAsync(string userName);
    Task<User?>GetUserByEmployeeIdAsync(string employeeId);
  }
}
