using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TitleDeedManagementSystem.Controllers
{
  [Authorize(Roles = "Delivery Checker")]
  public class TDDeliveryController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }
  }
}

