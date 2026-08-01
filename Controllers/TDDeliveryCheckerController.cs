using Microsoft.AspNetCore.Mvc;
using TitleDeedManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;


namespace TitleDeedManagementSystem.Controllers
{
  [Authorize(Roles = "Delivery Checker")]
  public class TDDeliveryCheckerController : Controller
  {

    private readonly IDataEntryService _dataEntryService;

    public TDDeliveryCheckerController(IDataEntryService dataEntryService)
    {
      _dataEntryService = dataEntryService;
    }

    public async Task<IActionResult> Index()
    {
      var entries = await _dataEntryService.GetPendingTdDeliveryAsync();

      return View(entries);
    }

    public async Task <IActionResult> Details(int id)
    {
      var entry = await _dataEntryService.GetTitleDeedDetailsByIdAsync(id);
      if(entry == null)
      {
        return NotFound();
      }
      return PartialView("_Details", entry);
    }

    [HttpPost]
    public async Task<IActionResult> Approve(int id)
    {
      await _dataEntryService.ApproveTdDeliveryAsync(id);
      TempData["DeliveryCheckerSuccess"] = "TD Delivery Approved Successfully";
      return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult>Reject(int id)
    {
      await _dataEntryService.RejectTdDeliveryAsync(id);
      TempData["DeliveryCheckerSuccess"] = "TD Delivery Rejected Successfully";
      return RedirectToAction(nameof(Index));
    }
  }
}
