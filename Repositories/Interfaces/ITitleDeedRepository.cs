using TitleDeedManagementSystem.Models;
using TitleDeedManagementSystem.Repositories.Implementations;

namespace TitleDeedManagementSystem.Repositories.Interfaces
{
  public interface ITitleDeedRepository
  {
    Task AddAsync(TitleDeedEntry titleDeedEntry);

    Task<TitleDeedEntry?>GetByCollateralIdAsync(int collateralId);


    Task UpdateTitleDeedEntryAsync(TitleDeedEntry model);

  }
}
