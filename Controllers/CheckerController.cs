using Microsoft.AspNetCore.Mvc;
using TitleDeedManagementSystem.Services.Interfaces;

namespace TitleDeedManagementSystem.Controllers
{
  public class CheckerController  : Controller
  {
    private readonly IDataEntryService _dataEntryService;

    public CheckerController(IDataEntryService dataEntryService)
    {
      _dataEntryService = dataEntryService;
    }

    public async Task<IActionResult> Index() {
      var entries = await _dataEntryService.GetSubmittedTitleDeedsAsync();
      return View(entries);
    }


  }
}

