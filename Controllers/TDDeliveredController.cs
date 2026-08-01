using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TitleDeedManagementSystem.Services.Interfaces;

namespace TitleDeedManagementSystem.Controllers
{
  [Authorize(Roles ="Delivery Checker")]
  public class TDDeliveredController : Controller
  {
    private readonly IDataEntryService _dataEntryService;

    public TDDeliveredController(IDataEntryService dataEntryService)
    {
      _dataEntryService = dataEntryService;
    }

    public async Task<IActionResult> Index()
    {
      var deliveredTitleDeeds = await _dataEntryService.GetDeliveredTitleDeedsAsync();
      return View(deliveredTitleDeeds);

    }

    public async Task<IActionResult> Details(int id)
    {
      var titleDeed = await _dataEntryService.GetTitleDeedDetailsByIdAsync(id);

      if(titleDeed == null)
      {
        return NotFound();
      }

      return PartialView("_Details", titleDeed);
    }
  }
}
