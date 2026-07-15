using TitleDeedManagementSystem.Models;

namespace TitleDeedManagementSystem.Repositories.Interfaces
{
  public interface IAccountRepository
  {
    Task<Account?> GetAccountByAccountNumberAsync(string accountNumber);
  }
}
