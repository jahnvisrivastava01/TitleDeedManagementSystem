using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TitleDeedManagementSystem.Services.Interfaces;

namespace TitleDeedManagementSystem.Controllers
{
  [Authorize(Roles = "Redeposit Checker")]
  public class TDCheckerController : Controller
  {
    private readonly IDataEntryService _dataEntryService;

    public TDCheckerController(IDataEntryService dataEntryService)
    {
      _dataEntryService = dataEntryService;
    }

    public async Task<IActionResult> Index()
    {
      var entries = await _dataEntryService.GetSubmittedTitleDeedsAsync();

      return View(entries);
    }
    



    public async Task<IActionResult> Details(int id)
    {
      var entry = await _dataEntryService.GetTitleDeedDetailsByIdAsync(id);
      if (entry == null)
      {
        return NotFound();
      }
      return PartialView("_Details", entry); ;
    }

    [HttpPost]
    public async Task<IActionResult> Approve(int id)
    {
      await _dataEntryService.ApproveTitleDeedAsync(id);

      TempData["Success"] = "Title Deed Approved Successfully";

      return RedirectToAction(nameof(Index));
    }


    [HttpPost]
    public async Task<IActionResult> Reject(int id)
    {
      await _dataEntryService.RejectTitleDeedAsync(id);

      TempData["Success"] = "Title Deed Rejected Successfully";
      return RedirectToAction(nameof(Index));
    }
  }
}
