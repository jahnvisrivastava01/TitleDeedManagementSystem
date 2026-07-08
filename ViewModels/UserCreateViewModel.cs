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

    public int DesignationId { get; set; }

    public int BranchId { get; set; }

    public int SelectedRoleId { get; set; }

    public bool IsActive { get; set; }

    public List<int> SelectedRoleIds { get; set; } = new List<int>();
  }
}
