using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TitleDeedManagementSystem.Services.Interfaces;
using TitleDeedManagementSystem.ViewModel;

namespace TitleDeedManagementSystem.Controllers
{
  [Authorize(Roles = "Maker")]
  public class CersaiController : Controller  
  {
    private readonly IDataEntryService _dataEntryService;

    public CersaiController(IDataEntryService dataEntryService)
    {
      _dataEntryService = dataEntryService;
    }

    public IActionResult Index()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> FetchAccount(DataEntryViewModel model)
    {
      var account = await _dataEntryService.GetAccountByAccountNumberAsync(model.AccountNumber);

      if (account == null)
      {
        ModelState.AddModelError("", "Account not found!");
        return View("Index", model);
      }

      model.CIFNumber = account.CIFNumber;
      model.AccountHolderName = account.AccountHolderName;
      model.ProductCode = account.ProductCode;
      model.LoanLimit = account.LoanLimit;
      model.OutstandingAmount  = account.OutstandingAmount;
      model.AccountOpenDate = account.AccountOpenDate.Value;

      var titleDeeds = await _dataEntryService.GetApprovedTitleDeedsByAccountIdAsync(account.AccountId);

      model.Collaterals = titleDeeds.Select(t => t.Collateral).ToList();

      return View("Index", model);
    }

    [HttpGet]
    public async Task<IActionResult> GetCersaiDetails(int collateralId)
    {
      var collateral = await _dataEntryService.GetCollateralByIdAsync(collateralId);
      if (collateral == null)
        return NotFound();

      var titleDeed = await _dataEntryService.GetTitleDeedEntryByCollateralIdAsync(collateralId);

      if (titleDeed == null)
        return NotFound();

      var model = new CersaiSatisfactionViewModel
      {
        TitleDeedEntryId = titleDeed.TitleDeedEntryId,
        CollateralId = collateral.CollateralId,
        CollateralNumber = collateral.CollateralNumber,
        TitleDeedNumber = titleDeed.TitleDeedNumber,
        CERSAIAssetId = titleDeed.CERSAIAssetId,
        CersaiSatisfactionDate = titleDeed.CersaiSatisfactionDate
      };

      return PartialView("_CersaiDetails", model);

    }

    [HttpPost]
    public async Task<IActionResult> SaveCersaiSatisfaction(CersaiSatisfactionViewModel model)
    {
      if (!ModelState.IsValid)
      {
        return PartialView("_CersaiDetails", model);
      }

      var titleDeed = await _dataEntryService
          .GetTitleDeedEntryByCollateralIdAsync(model.CollateralId);

      if (titleDeed == null)
      {
        ModelState.AddModelError("", "Title Deed Entry not found.");
        return PartialView("_CersaiDetails", model);
      }

      titleDeed.CersaiSatisfactionDate = model.CersaiSatisfactionDate;
      titleDeed.CersaiStatus = "Pending";
      titleDeed.ModifiedDate = DateTime.Now;

      await _dataEntryService.SaveCersaiSatisfactionAsync(titleDeed);

      TempData["CersaiSuccess"] = "CERSAI Satisfaction submitted successfully.";

      return RedirectToAction(nameof(Index));
    }

  }
}
