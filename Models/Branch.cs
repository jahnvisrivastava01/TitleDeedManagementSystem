using System.ComponentModel.DataAnnotations;

namespace TitleDeedManagementSystem.Models
{
  public class Branch
  {
    [Key]
    public int BranchId { get; set; }

    [Required]
    public string BranchName { get; set; }

    public ICollection<User> Users { get; set; }
  }
}
