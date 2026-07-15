using Microsoft.EntityFrameworkCore;
using TitleDeedManagementSystem.Models;
using TitleDeedManagementSystem.Repositories.Interfaces;
using TitleDeedManagementSystem.Services.Interfaces;



namespace TitleDeedManagementSystem.Services.Implementations
{
  public class DataEntryService : IDataEntryService
  {
    private readonly IAccountRepository _accountRepository;
    private readonly ICollateralRepository _collateralRepository;
    private readonly ITitleDeedEntryRepository _titleDeedEntryRepository;

    public DataEntryService(IAccountRepository accountRepository, ICollateralRepository collateralRepository,ITitleDeedEntryRepository titleDeedEntryRepository)
    {
      _accountRepository = accountRepository;
      _collateralRepository = collateralRepository;
      _titleDeedEntryRepository = titleDeedEntryRepository;
    }

    public async Task<Account?> GetAccountByAccountNumberAsync(string accountNumber)
    {
      return await _accountRepository.GetAccountByAccountNumberAsync(accountNumber);
    }

    public async Task<Collateral?>GetCollateralByIdAsync(int collateralId)
    {
      return await _collateralRepository.GetCollateralByIdAsync(collateralId);
    }

    public async Task SaveTitleDeedEntryAsync(TitleDeedEntry model)
    {
      await _titleDeedEntryRepository.SaveTitleDeedEntryAsync(model);
    }

    public async Task<TitleDeedEntry?> GetTitleDeedEntryByCollateralIdAsync(int collateralId)
    {
      return await _titleDeedEntryRepository
          .GetTitleDeedEntryByCollateralIdAsync(collateralId);
    }

    public async Task<List<TitleDeedEntry>> GetSubmittedTitleDeedsAsync()
    {
      return await _titleDeedEntryRepository.GetSubmittedTitleDeedsAsync();
    }



  }
}
