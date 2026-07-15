using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TitleDeedManagementSystem.Controllers
{
  [Authorize(Roles = "Redeposit Checker")]
  public class TDCheckerController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }
  }
}

