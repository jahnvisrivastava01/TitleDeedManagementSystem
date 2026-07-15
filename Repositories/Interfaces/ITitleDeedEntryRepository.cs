using TitleDeedManagementSystem.Models;

namespace TitleDeedManagementSystem.Repositories.Interfaces
{
  public interface ITitleDeedEntryRepository
  {
    Task SaveTitleDeedEntryAsync(TitleDeedEntry model);
    Task<TitleDeedEntry?> GetTitleDeedEntryByCollateralIdAsync(int collateralId);
    Task<List<TitleDeedEntry>> GetSubmittedTitleDeedsAsync();
   
  }
}
