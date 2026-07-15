using TitleDeedManagementSystem.Data;
using TitleDeedManagementSystem.Models;
using TitleDeedManagementSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using TitleDeedManagementSystem.Helpers.Enums;

namespace TitleDeedManagementSystem.Repositories.Implementations
{
  public class TitleDeedEntryRepository : ITitleDeedEntryRepository
  {
    private readonly ApplicationDbContext _context;

    public TitleDeedEntryRepository(ApplicationDbContext context)
    {
      _context = context;
    }
    public async Task SaveTitleDeedEntryAsync(TitleDeedEntry model)
    {
      _context.TitleDeedEntries.Add(model);
      await _context.SaveChangesAsync();
    }

    public async Task<TitleDeedEntry?> GetTitleDeedEntryByCollateralIdAsync(int collateralId)
    {
      return await _context.TitleDeedEntries
          .FirstOrDefaultAsync(t => t.CollateralId == collateralId);
    }

    public async Task<List<TitleDeedEntry>> GetSubmittedTitleDeedsAsync()
    {
      return await _context.TitleDeedEntries.Include(t => t.Collateral).Where(t => t.TitledeedStatus == TitledeedStatus.DATA_ENTRY_SUBMITTED).ToListAsync();
    }
   
  }
}
