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

    if (user.UserRoles.Any(r => r.Role.RoleName == "Branch Admin"))
    {
      return RedirectToAction("Index", "User");
    }

    if (user.UserRoles.Any(r => r.Role.RoleName == "Maker"))
    {
      return RedirectToAction("Index", "DataEntry");
    }

    if (user.UserRoles.Any(r => r.Role.RoleName == "Requisition Checker"))
    {
      return RedirectToAction("Index", "RequisitionChecker");
    }

    if (user.UserRoles.Any(r => r.Role.RoleName == "Redeposit Checker"))
    {
      return RedirectToAction("Index", "TDChecker");
    }

    if (user.UserRoles.Any(r => r.Role.RoleName == "CMM Checker"))
    {
      return RedirectToAction("Index", "CersaiCMMChecker");
    }

    if (user.UserRoles.Any(r => r.Role.RoleName == "Delivery Checker"))
    {
      return RedirectToAction("Index", "TDDeliveryChecker");
    }

    
    return RedirectToAction("Index", "Home");
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

  

public IActionResult GenerateHash()
{
  var helper = new PasswordHelper();
  var hash = helper.HashPassword("admin123");

  return Content(hash);
}
}
