using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using TitleDeedManagementSystem.Helpers;
using TitleDeedManagementSystem.Models;
using TitleDeedManagementSystem.Services;
using TitleDeedManagementSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
namespace TitleDeedManagementSystem.Controllers
{
  [Authorize]
  public class UserController : Controller
  {
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;
    private readonly IMasterDataService _masterDataService;
    private readonly PasswordHelper _passwordHelper;
    

    public UserController(
      IUserService userService,
      IMasterDataService masterDataService,
      ILogger<UserController> logger,
      PasswordHelper passwordHelper)
    {
      _userService = userService;
      _masterDataService = masterDataService;
      _logger = logger;
      _passwordHelper = passwordHelper;

    }

    public async Task<IActionResult> Index()
    {
      var users = await _userService.GetAllUsersAsync();
      return View(users);
    }

    public async Task<IActionResult> Create()
    {
      ViewBag.Branches = await _masterDataService.GetBranchesAsync();
      ViewBag.Designations = await _masterDataService.GetDesignationsAsync();
      ViewBag.Roles = await _masterDataService.GetRolesAsync();

      return View();

    
    }
    [HttpPost]
    public async Task<IActionResult> Create(UserCreateViewModel model)
    {
      if (await _userService.EmployeeIdExistsAsync(model.EmployeeId))
      {
        ModelState.AddModelError(nameof(model.EmployeeId), "Employee ID already exists!");
      }

      if (await _userService.UserNameExistsAsync(model.UserName))
      {
        ModelState.AddModelError(nameof(model.UserName), "User Name already exists!");
      }

      if ((model.DesignationId == 3 || model.DesignationId == 4) &&
          !model.SelectedRoleIds.Any())
      {
        ModelState.AddModelError(nameof(model.SelectedRoleIds),
            "Please select at least one Checker role.");
      }

      if (!ModelState.IsValid)
      {
        foreach (var item in ModelState)
        {
          foreach (var error in item.Value.Errors)
          {
            _logger.LogWarning("{Field}: {Message}",
                item.Key,
                error.ErrorMessage);
          }
        }

        ViewBag.Branches = await _masterDataService.GetBranchesAsync();
        ViewBag.Designations = await _masterDataService.GetDesignationsAsync();
        ViewBag.Roles = await _masterDataService.GetRolesAsync();

        return View(model);
      }

      var user = new User
      {
        EmployeeId = model.EmployeeId,
        UserName = model.UserName,
        Email = model.Email,
        PhoneNumber = model.PhoneNumber,
        BranchId = model.BranchId!.Value,
        DesignationId = model.DesignationId!.Value,
        IsActive = model.IsActive,
        CreatedOn = DateTime.Now,
        Password = _passwordHelper.HashPassword(model.Password)
      };

      await _userService.AddUserAsync(user);

      _logger.LogInformation(
          "User created successfully. EmployeeId: {EmployeeId}, UserName: {UserName}",
          user.EmployeeId,
          user.UserName);

      if (model.DesignationId == 1 || model.DesignationId == 2)
      {
        await _userService.AddUserRoleAsync(new UserRole
        {
          UserId = user.UserId,
          RoleId = 1
        });
      }
      else
      {
        foreach (var roleId in model.SelectedRoleIds)
        {
          await _userService.AddUserRoleAsync(new UserRole
          {
            UserId = user.UserId,
            RoleId = roleId
          });
        }
      }

      TempData["UserSuccess"] = "User Created successfully!";
      return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
      var user = await _userService.GetUserByIdAsync(id);

      if (user == null)
        return NotFound();

      ViewBag.Branches = await _masterDataService.GetBranchesAsync();
      ViewBag.Designations = await _masterDataService.GetDesignationsAsync();
      ViewBag.Roles = await _masterDataService.GetRolesAsync();
      return View(user);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(User user
      )
    {
      if (ModelState.IsValid)
      {
        user.ModifiedOn = DateTime.Now;
        await _userService.UpdateUserAsync(user);
        _logger.LogInformation("User updated. UserId: {UserId}", user.UserId);
        TempData["UserSuccess"] = "User updated successfully!";
        return RedirectToAction(nameof(Index));
      }
      ViewBag.Branches = await _masterDataService.GetBranchesAsync();
      ViewBag.Designations = await _masterDataService.GetDesignationsAsync();
      ViewBag.Roles = await _masterDataService.GetRolesAsync();

      return View(user);

    }
    public async Task<IActionResult> Delete(int id)
    {
      var user = await _userService.GetUserByIdAsync(id);
      if (user == null)
        return NotFound();
      user.IsActive = false;
      user.ModifiedOn = DateTime.Now;
      await _userService.UpdateUserAsync(user);
      _logger.LogInformation("User deactivated. UserId: {UserId}", user.UserId);
      TempData["UserSuccess"] = "User deactivated successfully!";
      return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> AssignRole(int id)
    {
      var user = await _userService.GetUserByIdAsync(id);
      if (user == null)
        return NotFound();
      ViewBag.Roles = await _masterDataService.GetRolesAsync();
      return View(user);
    }
    
    [HttpPost]
    public async Task<IActionResult> AssignRole(int userId, List<int> roleIds)
    {
      var user = await _userService.GetUserByIdAsync(userId);

      if (user == null)
        return NotFound();

     
      if (user.DesignationId == 1 || user.DesignationId == 2)
      {
        roleIds = new List<int> { 1 };   
      }

      
      if ((user.DesignationId == 3 || user.DesignationId == 4) &&
          !roleIds.Any())
      {
        ModelState.AddModelError("", "Please select at least one Checker role.");

        ViewBag.Roles = await _masterDataService.GetRolesAsync();

        return View(user);
      }

      await _userService.UpdateUserRoleAsync(userId, roleIds);
      _logger.LogInformation("Roles updated for UserId: {UserId}. Roles: {Roles}",
    userId, string.Join(", ", roleIds));

      TempData["UserSuccess"] = "Role assigned successfully!";

      return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Activate(int id)
    {
      await _userService.ActivateUserAsync(id);

      TempData["UserSuccess"] = "User activated successfully.";

      return RedirectToAction(nameof(Index));
    }

  }
}
