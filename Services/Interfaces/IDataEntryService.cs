using TitleDeedManagementSystem.Models;

namespace TitleDeedManagementSystem.Services.Interfaces
{
  public interface IDataEntryService
  {
    Task<Account?>GetAccountByAccountNumberAsync(string accountNumber);
    Task<Collateral?> GetCollateralByIdAsync(int collateralId);
    Task SaveTitleDeedEntryAsync(TitleDeedEntry model);
    Task<TitleDeedEntry?> GetTitleDeedEntryByCollateralIdAsync(int collateralId);

    Task<List<TitleDeedEntry>> GetSubmittedTitleDeedsAsync();


  }
}
