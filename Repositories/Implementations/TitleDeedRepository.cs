using Microsoft.EntityFrameworkCore;
using TitleDeedManagementSystem.Data;
using TitleDeedManagementSystem.Models;
using TitleDeedManagementSystem.Repositories.Interfaces;



namespace TitleDeedManagementSystem.Repositories.Implementations
{
  public class TitleDeedRepository : ITitleDeedRepository
  {
    private readonly ApplicationDbContext _context;

    public TitleDeedRepository(ApplicationDbContext context) {
      _context = context;
    }

    public async Task AddAsync(TitleDeedEntry titleDeedEntry)
    {
      await _context.TitleDeedEntries.AddAsync(titleDeedEntry);
      await _context.SaveChangesAsync();
    }

    public async Task<TitleDeedEntry?> GetByCollateralIdAsync(int collateralId)
    {
      return await _context.TitleDeedEntries
        .FirstOrDefaultAsync(t => t.CollateralId == collateralId);
    }

    public Task UpdateTitleDeedEntryAsync(TitleDeedEntry model)
    {
      throw new NotImplementedException();
    }
  }
}
