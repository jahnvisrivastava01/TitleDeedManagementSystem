using System.ComponentModel.DataAnnotations;

namespace TitleDeedManagementSystem.ViewModels
{
  public class UserCreateViewModel
  {
    public int UserId { get; set; }

    [Required]
    public string EmployeeId { get; set; }

    [Required]
    public string UserName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(15)]
    public string PhoneNumber { get; set; } = string.Empty;



    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }  

    [Required]
    public int DesignationId { get; set; }

    [Required]
    public int BranchId { get; set; }

    public bool IsActive { get; set; }

    
    public int SelectedRoleId { get; set; }

    
    public List<int> SelectedRoleIds { get; set; } = new();
  }
}
