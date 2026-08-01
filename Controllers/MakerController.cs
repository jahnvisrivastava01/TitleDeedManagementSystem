using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TitleDeedManagementSystem.Controllers
{
  [Authorize(Roles = "Maker")]
  public class MakerController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }

  }
}







