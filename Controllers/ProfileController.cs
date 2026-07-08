using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using TitleDeedManagementSystem.Services;
using TitleDeedManagementSystem.ViewModels;
using TitleDeedManagementSystem.ViewModel;


namespace TitleDeedManagementSystem.Controllers
{
  [Authorize]
  public class ProfileController : Controller
  {
    private readonly IUserService _userService;
    public ProfileController(IUserService userService) {

      _userService = userService;
    }
    public async Task <IActionResult> Index()
    {
      var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      if (string.IsNullOrEmpty(userIdClaim))
      {
        return RedirectToAction("LoginBasic","Auth");
      }

      int userId = int.Parse(userIdClaim);

      var user = await _userService.GetUserByIdAsync(userId);
      if (user == null)
      {
        return NotFound();
      }

      var model = new ProfileViewModel
      {
        UserName = user.UserName,
        Email = user.Email,
        PhoneNumber = user.PhoneNumber,
        Role = user.UserRoles.FirstOrDefault()?.Role?.RoleName ?? ""
      };

      return View(model);
    }
  }
}
