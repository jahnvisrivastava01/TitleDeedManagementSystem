using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TitleDeedManagementSystem.Models
{
  public class User
  {
    [Key]
    public int UserId { get; set; }

    [Required]
    [StringLength(20)]
    public string EmployeeId { get; set; }

    [Required]
    [StringLength(100)]
    public string UserName { get; set; }

    [Required]
    [StringLength(100)]
    public string Email { get; set; }

    [Required]
    [StringLength(15)]
    public string PhoneNumber { get; set; }

    [Required]
    [StringLength(500)]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    public int DesignationId { get; set; }

    [ValidateNever]
    public Designation Designation { get; set; }

    [Required]
    public int BranchId { get; set; }

    [ValidateNever]
    public Branch Branch { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedOn { get; set; } = DateTime.Now;

    public DateTime? ModifiedOn { get; set; }

    [ValidateNever]
    public ICollection<UserRole> UserRoles { get; set; }

 
  }
}
