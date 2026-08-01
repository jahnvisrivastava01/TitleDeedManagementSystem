using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TitleDeedManagementSystem.Helpers.Enums;
using TitleDeedManagementSystem.Models;

using TitleDeedManagementSystem.Services;
using TitleDeedManagementSystem.Services.Implementations;
using TitleDeedManagementSystem.Services.Interfaces;
using TitleDeedManagementSystem.ViewModel;
using System.Linq;


namespace TitleDeedManagementSystem.Controllers
{
  public class DataEntryController : Controller
  {
    private readonly IDataEntryService _dataEntryService;
    private readonly IMasterDataService _masterDataService;

    public DataEntryController(
      IDataEntryService dataEntryService,
      IMasterDataService masterDataService)
    {
      _dataEntryService = dataEntryService;
      _masterDataService = masterDataService;
    }

   

    public IActionResult Index()
    {
      return View(new DataEntryViewModel());
    }

    [HttpPost]
    public async Task<IActionResult>
    FetchAccount(DataEntryViewModel model)
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
      model.OutstandingAmount = account.OutstandingAmount;
      model.AccountOpenDate = account.AccountOpenDate.Value;
      model.Collaterals = account.Collaterals.ToList();

      return View("Index", model);
    }

    [HttpGet]
    public async Task<IActionResult> GetCollateral(int collateralId)
    {
      var collateral = await _dataEntryService.GetCollateralByIdAsync(collateralId);

      if (collateral == null)
        return NotFound();

      var titleDeed = await _dataEntryService
          .GetTitleDeedEntryByCollateralIdAsync(collateralId);

      

      var model = new TitleDeedEntryViewModel
      {
        AccountId = collateral.AccountId,
        CollateralId = collateral.CollateralId,
        CollateralNumber = collateral.CollateralNumber,

        Compactors = (await _masterDataService.GetCompactorsAsync())
        .Select(c => new SelectListItem
        {
          Value = c.CompactorId.ToString(),
          Text = c.CompactorName
        })
      };




      if (titleDeed != null)
      {
        model.TitleDeedAvailable = titleDeed.IsTitleDeedAvailable;
        model.TitleDeedNumber = titleDeed.TitleDeedNumber;
        model.EMRegisterNumber = titleDeed.EMRegisterNumber;
        model.EMFolioNumber = titleDeed.EMFolioNumber;
        model.FileNumber = titleDeed.FileNumber;
        model.CERSAIAssetId = titleDeed.CERSAIAssetId;
        model.CompactorId = titleDeed.CompactorId;
        model.RackId = titleDeed.RackId;
      }

      return PartialView("_CollateralDetails", model);
    }

    [HttpGet]
    public async Task<JsonResult> GetRacks(int compactorId)
    {
      var racks = await _masterDataService.GetRacksByCompactorAsync(compactorId);

      return Json(racks);
    }

    [HttpPost]
    public async Task<IActionResult> SaveTitleDeedEntry(TitleDeedEntryViewModel model)
    {
      if (!ModelState.IsValid)
      {
        model.Compactors = (await _masterDataService.GetCompactorsAsync())
            .Select(c => new SelectListItem
            {
              Value = c.CompactorId.ToString(),
              Text = c.CompactorName
            });

        return PartialView("_CollateralDetails", model);
      }

      var existingEntry = await _dataEntryService.GetTitleDeedEntryByCollateralIdAsync(model.CollateralId);
      if(existingEntry != null)
      {
        ModelState.AddModelError("", "Title Deed Entry already exists for this collateral !");
        
        model.Compactors = (await _masterDataService.GetCompactorsAsync())
          .Select(c => new SelectListItem
          {
            Value = c.CompactorId.ToString(),
            Text = c.CompactorName
          });
        return PartialView("_CollateralDetails", model);
      }

      var titleDeed = new TitleDeedEntry
      {
        CollateralId = model.CollateralId,
        IsTitleDeedAvailable = model.TitleDeedAvailable,
        TitleDeedNumber = model.TitleDeedNumber!,
        EMRegisterNumber = model.EMRegisterNumber!,
        EMFolioNumber = model.EMFolioNumber!,
        FileNumber = model.FileNumber!,
        CERSAIAssetId = model.CERSAIAssetId!,
        CompactorId = model.CompactorId!.Value,
        RackId = model.RackId!.Value,

        CreatedBy = 1,
        CreatedDate = DateTime.Now,
        ModifiedDate = null,

        TitledeedStatus = TitledeedStatus.DATA_ENTRY_SUBMITTED
      };

      await _dataEntryService.SaveTitleDeedEntryAsync(titleDeed);

      TempData["DataEntrySuccess"] = "Title Deed Entry Submitted Successfully.";

      return RedirectToAction(nameof(Index));
    }




  }
}
