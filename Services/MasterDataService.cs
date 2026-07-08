using Microsoft.EntityFrameworkCore;
using TitleDeedManagementSystem.Data;
using TitleDeedManagementSystem.Models;

namespace TitleDeedManagementSystem.Services
{
  public class MasterDataService : IMasterDataService
  {
    private readonly ApplicationDbContext _context;

    public MasterDataService(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<IEnumerable<Branch>> GetBranchesAsync()
    {
      return await _context.Branches.ToListAsync();
    }

    public async Task<IEnumerable<Designation>> GetDesignationsAsync()
    {
      return await _context.Designations.ToListAsync();
    }

    public async Task<IEnumerable<Role>> GetRolesAsync()
    {
      return await _context.Roles.ToListAsync();
    }
  }
}
