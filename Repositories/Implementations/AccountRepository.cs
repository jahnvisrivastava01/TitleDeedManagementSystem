using Microsoft.EntityFrameworkCore;
using TitleDeedManagementSystem.Data;
using TitleDeedManagementSystem.Models;
using TitleDeedManagementSystem.Repositories.Interfaces;

namespace TitleDeedManagementSystem.Repositories.Implementations
{
  public class AccountRepository : IAccountRepository
  {
    private readonly ApplicationDbContext _context;

    public AccountRepository(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<Account?> GetAccountByAccountNumberAsync(string accountNumber)
    {
      return await _context.Accounts.Include(a => a.BranchSettings).Include(a => a.Collaterals).FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
    }
  }
}
