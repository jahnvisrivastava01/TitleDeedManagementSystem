using TitleDeedManagementSystem.Helpers.Enums;
using TitleDeedManagementSystem.Models;

namespace TitleDeedManagementSystem.Repositories.Interfaces
{
  public interface ITitleDeedEntryRepository
  {
    Task SaveTitleDeedEntryAsync(TitleDeedEntry model);
    Task<TitleDeedEntry?> GetTitleDeedEntryByCollateralIdAsync(int collateralId);
    Task<List<TitleDeedEntry>> GetSubmittedTitleDeedsAsync();
    Task<TitleDeedEntry?> GetTitleDeedDetailsByIdAsync(int id);

    Task ApproveTitleDeedAsync(int titleDeedEntryId);

    Task RejectTitleDeedAsync(int titleDeedEntryId);

    Task <List<TitleDeedEntry>>GetApprovedTitleDeedsAsync();
    Task SaveCersaiSatisfactionAsync(TitleDeedEntry model);

    Task<List<TitleDeedEntry>> GetPendingCersaiAsync();

    Task ApproveCersaiAsync(int titleDeedEntryId);
    Task RejectCersaiAsync(int titleDeedEntryId);

    Task<List<TitleDeedEntry>> GetApprovedTitleDeedsByAccountIdAsync(int accountId);

    Task<List<TitleDeedEntry>> GetEligibleTitleDeedsForTdDeliveryAsync(int accountId);

    Task SaveTdDeliveryAsync(TitleDeedEntry model);

    Task<List<TitleDeedEntry>> GetPendingTdDeliveryAsync();
    Task ApproveTdDeliveryAsync(int titleDeedEntryId);

    Task RejectTdDeliveryAsync(int titleDeedEntryId);
    Task<List<TitleDeedEntry>> GetDeliveredTitleDeedsAsync();




  }
}
