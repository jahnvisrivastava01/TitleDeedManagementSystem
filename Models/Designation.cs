using System.ComponentModel.DataAnnotations;

namespace TitleDeedManagementSystem.Models
{
  public class Designation
  {
    [Key]
    public int DesignationId { get; set; }

    [Required]
    public string DesignationName { get; set; }

    public ICollection<User> Users { get; set; }
  }
}
