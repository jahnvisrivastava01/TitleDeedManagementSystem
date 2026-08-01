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

      entry.CersaiStatus = "Approved";
      entry.ModifiedDate = DateTime.Now;   

      await _context.SaveChangesAsync();
    }

    public async Task RejectCersaiAsync(int titleDeedEntryId)
    {
      var entry = await _context.TitleDeedEntries
          .FirstOrDefaultAsync(t => t.TitleDeedEntryId == titleDeedEntryId);

      if (entry == null)
        return;

      entry.CersaiStatus = "Rejected";
      entry.ModifiedDate = DateTime.Now;

      await _context.SaveChangesAsync();
    }

    public async Task<List<TitleDeedEntry>> GetApprovedTitleDeedsByAccountIdAsync(int accountId)
    {
      return await _context.TitleDeedEntries
          .Include(t => t.Collateral)
          .Where(t =>
              t.Collateral.AccountId == accountId &&
              t.TitledeedStatus == TitledeedStatus.DATA_ENTRY_APPROVED &&
              (t.CersaiStatus == null || t.CersaiStatus == "Rejected"))
          .ToListAsync();
    }



    public async Task<List<TitleDeedEntry>> GetEligibleTitleDeedsForTdDeliveryAsync(int accountId)
    {
      return await _context.TitleDeedEntries
          .Include(t => t.Collateral)
          .Where(t =>
              t.Collateral.AccountId == accountId &&
              t.TitledeedStatus == TitledeedStatus.DATA_ENTRY_APPROVED &&
              t.CersaiStatus == "Approved" &&
              t.CersaiSatisfactionDate != null &&
              (t.TdDeliveryStatus == null || t.TdDeliveryStatus == "Rejected"))
          .ToListAsync();
    }
    public async Task SaveTdDeliveryAsync(TitleDeedEntry model)
    {
      var entry = await _context.TitleDeedEntries
          .FirstOrDefaultAsync(t => t.TitleDeedEntryId == model.TitleDeedEntryId);

      if (entry == null)
        return;

      entry.TdDeliveryRaisedDate = model.TdDeliveryRaisedDate;
      entry.TdDeliveryStatus = model.TdDeliveryStatus;
      entry.ModifiedDate = model.ModifiedDate;

      await _context.SaveChangesAsync();
    }

    public async Task<List<TitleDeedEntry>> GetPendingTdDeliveryAsync() {
      return await _context.TitleDeedEntries
        .Include(t => t.Collateral).ThenInclude(c => c.Account).Where(t => t.TdDeliveryStatus == "Pending").ToListAsync();




    }

    public async Task ApproveTdDeliveryAsync(int titleDeedEntryId)
    {
      var entry = await _context.TitleDeedEntries.FirstOrDefaultAsync(t => t.TitleDeedEntryId == titleDeedEntryId);

      if (entry == null)
        return;

      entry.TdDeliveryStatus = "Approved";
      entry.ModifiedDate = DateTime.Now;

      await _context.SaveChangesAsync();
    }


    public async Task RejectTdDeliveryAsync(int titleDeedEntryId) {

      var entry = await _context.TitleDeedEntries.FirstOrDefaultAsync(t => t.TitleDeedEntryId == titleDeedEntryId);

      if (entry == null)
        return;

      entry.TdDeliveryStatus = "Rejected";
      entry.ModifiedDate = DateTime.Now;

      await _context.SaveChangesAsync();



    }

    public async Task<List<TitleDeedEntry>> GetDeliveredTitleDeedsAsync()
    {
      return await _context.TitleDeedEntries
          .Include(t => t.Collateral)
              .ThenInclude(c => c.Account)
          .Where(t => t.TdDeliveryStatus == "Approved")
          .OrderByDescending(t => t.ModifiedDate)
          .ToListAsync();
    }




  }
}
