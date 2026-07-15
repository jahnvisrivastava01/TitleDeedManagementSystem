using Microsoft.AspNetCore.Mvc;
using TitleDeedManagementSystem.ViewModel;
using TitleDeedManagementSystem.ViewModels;
using TitleDeedManagementSystem.Services;
using TitleDeedManagementSystem.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace TitleDeedManagementSystem.Controllers;

public class AuthController : Controller
{
  private readonly IUserService _userService;
  private readonly PasswordHelper _passwordHelper;


  public AuthController(
    IUserService userService,
    PasswordHelper passwordHelper)
  {
    _userService = userService;
    _passwordHelper = passwordHelper;
  }

  [HttpGet]
  public IActionResult LoginBasic()
  {
    return View();
  }

  [HttpPost]
  public async Task<IActionResult> LoginBasic(LoginViewModel model)
  {
    if (!ModelState.IsValid)
    {
      return View(model);
    }
    var user = await _userService.GetUserByEmployeeIdAsync(model.EmployeeId);

    if (user == null)
    {
      ModelState.AddModelError("", "Invalid Employee Id or Password");
      return View(model);
    }

    bool isPasswordValid = _passwordHelper.VerifyPassword(user.Password, model.Password);
    if (!isPasswordValid)
    {
      ModelState.AddModelError("", "Invalid Employee ID or Password.");
      return View(model);
    }
    var claims = new List<Claim>
    {
      new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
      new Claim(ClaimTypes.Name, user.UserName),
      new Claim("EmployeeId",user.EmployeeId),
      new Claim("Email", user.Email),
      new Claim("PhoneNumber", user.PhoneNumber)

     
    };

    foreach (var userRole in user.UserRoles)
    {
      claims.Add(new Claim(ClaimTypes.Role,userRole.Role.RoleName));
    }

    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
    var principal = new ClaimsPrincipal(identity);

    await HttpContext.SignInAsync(
    CookieAuthenticationDefaults.AuthenticationScheme,
    principal);

    return RedirectToAction("Index", "User");
  }





  [Authorize]
  public async Task<IActionResult> Logout()

  {
    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return RedirectToAction(nameof(LoginBasic));
  }

  public IActionResult AccessDenied()
  {
    return View();
  }
}
