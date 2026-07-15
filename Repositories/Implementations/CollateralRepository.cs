using Microsoft.EntityFrameworkCore;
using TitleDeedManagementSystem.Data;
using TitleDeedManagementSystem.Models;
using TitleDeedManagementSystem.Repositories.Interfaces;


namespace TitleDeedManagementSystem.Repositories.Implementations
{
  public class CollateralRepository : ICollateralRepository
  {
    private readonly ApplicationDbContext _context;

    public CollateralRepository(ApplicationDbContext context) {
      _context = context;
    }

    public async Task<List<Collateral>> GetCollateralsByAccountIdAsync(int accountId)
    {
      return await _context.Collaterals.Where(c=>c.AccountId == accountId).Include(c=>c.TitleDeedEntry).ToListAsync();
    }

    public async Task<Collateral?> GetCollateralByIdAsync(int collateralId)
    {
      return await _context.Collaterals
          .FirstOrDefaultAsync(c => c.CollateralId == collateralId);
    }
  }
}
