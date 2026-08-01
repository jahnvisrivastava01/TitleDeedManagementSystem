using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TitleDeedManagementSystem.Services.Interfaces;

namespace TitleDeedManagementSystem.Controllers
{
  [Authorize(Roles = "CMM Checker")]
  public class CersaiCMMCheckerController : Controller
  {
    private readonly IDataEntryService _dataEntryService;

    public CersaiCMMCheckerController(IDataEntryService dataEntryService)
    {
      _dataEntryService = dataEntryService;
    }

    public async Task<IActionResult> Index()
    {
      var entries = await _dataEntryService.GetPendingCersaiAsync();

      return View(entries);
    }

    public async Task<IActionResult> Details(int id)
    {
      var entry = await _dataEntryService.GetTitleDeedDetailsByIdAsync(id);

      if (entry == null)
      {
        return NotFound();
      }

      return PartialView("_Details", entry);
    }

    [HttpPost]
    public async Task<IActionResult> Approve(int id)
    {
      await _dataEntryService.ApproveCersaiAsync(id);

      TempData["CMMSuccess"] = "CERSAI Satisfaction Approved Successfully.";

      return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Reject(int id)
    {
      await _dataEntryService.RejectCersaiAsync(id);

      TempData["CMMSuccess"] = "CERSAI Satisfaction Rejected Successfully.";

      return RedirectToAction(nameof(Index));
    }
  }
}
