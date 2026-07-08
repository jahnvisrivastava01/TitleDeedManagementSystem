using TitleDeedManagementSystem.Models;

namespace TitleDeedManagementSystem.Services
{
  public interface IMasterDataService
  {
    Task<IEnumerable<Branch>> GetBranchesAsync();

    Task<IEnumerable<Designation>> GetDesignationsAsync();

    Task<IEnumerable<Role>> GetRolesAsync();
  }
}
