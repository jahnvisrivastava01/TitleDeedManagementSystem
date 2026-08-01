using Microsoft.EntityFrameworkCore;
using TitleDeedManagementSystem.Models;
using TitleDeedManagementSystem.Repositories;

namespace TitleDeedManagementSystem.Services
{
  public class UserService : IUserService
  {
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
      _userRepository = userRepository;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
      return await _userRepository.GetAllUsersAsync();
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
      return await _userRepository.GetUserByIdAsync(id);
    }

    public async Task AddUserAsync(User user)
    {
      
      await _userRepository.AddUserAsync(user);
    }

    public async Task UpdateUserAsync(User user)
    {
      await _userRepository.UpdateUserAsync(user);
    }

    public async Task DeleteUserAsync(int id)
    {
      await _userRepository.DeleteUserAsync(id);
    }

    public async Task AddUserRoleAsync(UserRole userRole)
    {
      await _userRepository.AddUserRoleAsync(userRole);
    }

    public async Task UpdateUserRoleAsync(int userId, List<int> roleIds)
    {
      await _userRepository.UpdateUserRoleAsync(userId, roleIds);
    }

    public async Task<bool> EmployeeIdExistsAsync(string employeeId)
    {
      return await _userRepository.EmployeeIdExistsAsync(employeeId);

    }

    public async Task<bool> UserNameExistsAsync(string userName)
    {
      return await _userRepository.UserNameExistsAsync( userName);
    }

    public async Task<User?> GetUserByEmployeeIdAsync(string employeeId)
    {
      return await _userRepository.GetUserByEmployeeIdAsync(employeeId);
    }

    public async Task ActivateUserAsync(int id)
    {
      await _userRepository.ActivateUserAsync(id);
    }

  }
  
  }
