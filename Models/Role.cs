using System.ComponentModel.DataAnnotations;

namespace TitleDeedManagementSystem.Models
{
  public class Role
  {
    [Key]
    public int RoleId { get; set; }
    [Required]
    [StringLength(60)]
    public string RoleName { get; set; }

    public ICollection<UserRole>UserRoles { get; set; }
  }
}
