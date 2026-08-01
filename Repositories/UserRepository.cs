using TitleDeedManagementSystem.Data;
using TitleDeedManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace TitleDeedManagementSystem.Repositories
{
  public class UserRepository : IUserRepository
  {
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
      _context = context;
    }
    public async Task<User?> GetUserByIdAsync(int id
      )
    {
      return await _context.Users.Include(u => u.Designation).Include(u => u.Branch).Include(u=>u.UserRoles).ThenInclude(ur=>ur.Role).FirstOrDefaultAsync(u => u.UserId == id);
    }
    public async Task AddUserAsync(User user)
    {
      _context.Users.Add(user);
      await _context.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(int id)
    {
      var user = await _context.Users.FindAsync(id);
      if (user != null)
      {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
      }
    }
    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
      return await _context.Users
          .Include(u => u.Designation)
          .Include(u => u.Branch)
          .Include(u=>u.UserRoles)
              .ThenInclude(ur=>ur.Role)
          .ToListAsync();
    }

    public async Task UpdateUserAsync(User user)
    {
      _context.Users.Update(user);
      await _context.SaveChangesAsync();
    }

    public async Task AddUserRoleAsync(UserRole userRole)
    {
      _context.UserRoles.Add(userRole);
      await _context.SaveChangesAsync();
    }

    public async Task UpdateUserRoleAsync(int userId, List<int> roleIds)
    {
      var existingRoles = _context.UserRoles.Where(ur => ur.UserId == userId);

      _context.UserRoles.RemoveRange(existingRoles);

      foreach (var roleId in roleIds)
      {
        _context.UserRoles.Add(new UserRole
        {
          UserId = userId,
          RoleId = roleId
        });
      }

      await _context.SaveChangesAsync();
    }

   
    public async Task<bool>EmployeeIdExistsAsync(string employeeId)
    {
      return await _context.Users.AnyAsync(u => u.EmployeeId == employeeId);

    }

    public async Task<bool>UserNameExistsAsync(string userName)
    {
      return await _context.Users.AnyAsync(u => u.UserName == userName);
    }



    public async Task<User?> GetUserByEmployeeIdAsync(string employeeId)
    {
      return await _context.Users
          .Include(u => u.UserRoles)
              .ThenInclude(ur => ur.Role)
          .FirstOrDefaultAsync(u => u.EmployeeId == employeeId);
    }

    public async Task ActivateUserAsync(int id)
    {
      var user = await _context.Users.FindAsync(id);

      if (user != null)
      {
        user.IsActive = true;

        await _context.SaveChangesAsync();
      }
    }


  }
}
