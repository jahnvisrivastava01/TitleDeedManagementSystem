using System.ComponentModel.DataAnnotations;

namespace TitleDeedManagementSystem.ViewModels
{
  public class UserCreateViewModel
  {
    public int UserId { get; set; }

    [Required(ErrorMessage = "Employee ID is required.")]
    [RegularExpression(@"^\d+$", ErrorMessage = "Employee ID should contain only numbers.")]
    public string EmployeeId { get; set; } = string.Empty;

    [Required(ErrorMessage = "User Name is required.")]
    [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "User Name should contain only alphabets.")]
    [StringLength(100, ErrorMessage = "User Name cannot exceed 100 characters.")]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Enter a valid email address.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone Number is required.")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone Number must be exactly 10 digits.")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    [StringLength(100, MinimumLength = 8,
        ErrorMessage = "Password must be between 8 and 100 characters.")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Designation is required.")]
    public int? DesignationId { get; set; }

    [Required(ErrorMessage = "Branch is required.")]
    public int? BranchId { get; set; }

    public bool IsActive { get; set; }

    public List<int> SelectedRoleIds { get; set; } = new();
  }
}
