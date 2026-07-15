using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TitleDeedManagementSystem.Controllers
{
  [Authorize(Roles = "Requisition Checker")]
  public class RequisitionCheckerController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }

  }
}
