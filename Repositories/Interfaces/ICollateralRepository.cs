using TitleDeedManagementSystem.Models;

namespace TitleDeedManagementSystem.Repositories.Interfaces
{
  public interface ICollateralRepository
  {
    Task<List<Collateral>> GetCollateralsByAccountIdAsync(int accountId);
    Task<Collateral?> GetCollateralByIdAsync(int collateralId);

  }
}
