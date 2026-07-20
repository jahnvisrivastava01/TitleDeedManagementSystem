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
      return await _context.TitleDeedEntries.Include(t => t.Collateral).ThenInclude(c=>c.Account).Where(t => t.TitledeedStatus == TitledeedStatus.DATA_ENTRY_SUBMITTED).ToListAsync();
    }

    public async Task<TitleDeedEntry?> GetTitleDeedDetailsByIdAsync(int id)
    {
      return await _context.TitleDeedEntries
          .Include(t => t.Collateral)
              .ThenInclude(c => c.Account)
          .Include(t => t.Compactor)
          .Include(t => t.Rack)
          .FirstOrDefaultAsync(t => t.TitleDeedEntryId == id);
    }
    public async Task ApproveTitleDeedAsync(int titleDeedEntryId)
    {
      var entry = await _context.TitleDeedEntries
          .FirstOrDefaultAsync(t => t.TitleDeedEntryId == titleDeedEntryId);

      if (entry == null)
        return;

      entry.TitledeedStatus = TitledeedStatus.DATA_ENTRY_APPROVED;
      await _context.SaveChangesAsync();
    }

    public async Task RejectTitleDeedAsync(int titleDeedEntryId)
    {
      var entry = await _context.TitleDeedEntries.FirstOrDefaultAsync(t => t.TitleDeedEntryId == titleDeedEntryId);
      if (entry == null)
        return;

      entry.TitledeedStatus = TitledeedStatus.DATA_ENTRY_REJECTED;

      await _context.SaveChangesAsync();
    }

    public async Task<List<TitleDeedEntry>> GetApprovedTitleDeedsAsync()
    {
      return await _context.TitleDeedEntries
          .Include(t => t.Collateral)
          .ThenInclude(c => c.Account)
          .Where(t =>
              t.TitledeedStatus == TitledeedStatus.DATA_ENTRY_APPROVED &&
              (t.CersaiStatus == null || t.CersaiStatus == "Rejected"))
          .ToListAsync();
    }

    public async Task SaveCersaiSatisfactionAsync(TitleDeedEntry model)
    {
      var entry = await _context.TitleDeedEntries.FirstOrDefaultAsync(t => t.TitleDeedEntryId == model.TitleDeedEntryId);

      if (entry == null)
        return;

      entry.CersaiSatisfactionDate = model.CersaiSatisfactionDate;
      entry.CersaiStatus = "Pending";

      await _context.SaveChangesAsync();

    }
    public async Task<List<TitleDeedEntry>> GetPendingCersaiAsync()
    {
      return await _context.TitleDeedEntries
        .Include(t => t.Collateral)
            .ThenInclude(c => c.Account)
        .Where(t => t.CersaiStatus == "Pending")
        .ToListAsync();

    }
    

    public async Task ApproveCersaiAsync(int titleDeedEntryId)
    {
      var entry = await _context.TitleDeedEntries
          .FirstOrDefaultAsync(t => t.TitleDeedEntryId == titleDeedEntryId);

      if (entry == null)
        return;

      entry.TitledeedStatus = TitledeedStatus.DATA_ENTRY_APPROVED;
      await _context.SaveChangesAsync();
    }

    public async Task RejectCersaiAsync(int titleDeedEntryId)
    {
      var entry = await _context.TitleDeedEntries
          .FirstOrDefaultAsync(t => t.TitleDeedEntryId == titleDeedEntryId);

      if (entry == null)
        return;

      entry.CersaiStatus = "Rejected";
      await _context.SaveChangesAsync();

    }

    

  }
}
