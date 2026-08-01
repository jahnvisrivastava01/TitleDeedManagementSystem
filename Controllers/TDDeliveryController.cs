using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TitleDeedManagementSystem.Services.Interfaces;
using TitleDeedManagementSystem.ViewModel;

namespace TitleDeedManagementSystem.Controllers
{
  [Authorize(Roles = "Maker")]
  public class TDDeliveryController : Controller
  {
    private readonly IDataEntryService _dataEntryService;

    public TDDeliveryController(IDataEntryService dataEntryService)
    {
      _dataEntryService = dataEntryService;
    }

    public IActionResult Index()
    {
      return View(new DataEntryViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> FetchAccount(DataEntryViewModel model)
    {
      var account = await _dataEntryService.GetAccountByAccountNumberAsync(model.AccountNumber);

      if (account == null)
      {
        ModelState.AddModelError("", "account not found!");
        return View("Index", model);
      }



      model.CIFNumber = account.CIFNumber ?? "";
      model.AccountHolderName = account.AccountHolderName ?? "";
      model.ProductCode = account.ProductCode ?? "";
      model.LoanLimit = account.LoanLimit;
      model.OutstandingAmount = account.OutstandingAmount;

      if (account.AccountOpenDate.HasValue)
      {
        model.AccountOpenDate = account.AccountOpenDate.Value;
      }

      var titleDeeds = await _dataEntryService.GetEligibleTitleDeedsForTdDeliveryAsync(account.AccountId);

      

      if (!titleDeeds.Any())
      {
        ModelState.AddModelError("", "No eligible title deeds are available for TD Delivery ");
        return View("Index", model);
      }



      
      model.Collaterals = titleDeeds.Select(t => t.Collateral).Where(c => c != null).ToList()!;
      return View("Index", model);
    }

    [HttpGet]
    public async Task<IActionResult> GetTDDeliveryDetails(int collateralId)
    {
      var collateral = await _dataEntryService.GetCollateralByIdAsync(collateralId);
      if(collateral == null)
      {
        return NotFound();
      }

      var titleDeed = await _dataEntryService.GetTitleDeedEntryByCollateralIdAsync(collateralId);
      if(titleDeed == null) {
        return NotFound();
      }

      var model = new TDDeliveryViewModel
      {
        TitleDeedEntryId = titleDeed.TitleDeedEntryId,
        CollateralId = collateral.CollateralId,
        CollateralNumber = collateral.CollateralNumber,
        TitleDeedNumber = titleDeed.TitleDeedNumber,
        CERSAIAssetId = titleDeed.CERSAIAssetId,
        TdDeliveryRaisedDate = titleDeed.TdDeliveryRaisedDate
      };

      return PartialView("_TDDeliveryDetails", model);
    }

    [HttpPost]
    public async Task<IActionResult> SaveTDDelivery(TDDeliveryViewModel model)
    {
      if (!ModelState.IsValid)
      {
        return PartialView("_TDDeliveryDetails", model);
      }
      var titleDeed = await _dataEntryService.GetTitleDeedEntryByCollateralIdAsync(model.CollateralId);

      if (titleDeed == null)
      {
        ModelState.AddModelError("", "Title Deed Entry not found");
        return PartialView("_TDDeliveryDetails", model);
      }

      titleDeed.TdDeliveryStatus = "Pending";
      titleDeed.TdDeliveryRaisedDate = DateTime.Now;

      if (titleDeed.TdDeliveryStatus == "Pending" ||
    titleDeed.TdDeliveryStatus == "Approved")
      {
        ModelState.AddModelError("", "TD Delivery has already been raised for this title deed.");
        return PartialView("_TDDeliveryDetails", model);
      }

      titleDeed.TdDeliveryStatus = "Pending";
      titleDeed.TdDeliveryRaisedDate = DateTime.Now;
      titleDeed.ModifiedDate = DateTime.Now;

      await _dataEntryService.SaveTdDeliveryAsync(titleDeed);


      TempData["Success"] = "TD Delivery submitted successfully";

      return RedirectToAction(nameof(Index));

      
      
    }
  }
}

