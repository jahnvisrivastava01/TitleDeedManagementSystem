using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TitleDeedManagementSystem.Models
{
  public class BranchSettings
  {

    [Key]
    public int BranchSettingId { get; set; }

    [Required]
    [StringLength(20)]
    public string BranchCode { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string BranchName { get; set; } = string.Empty;

    [StringLength(200)]
    public string? BranchAddress { get; set; }

    [StringLength(100)]
    public string? Circle { get; set; }

    [StringLength(100)]
    public string? Region { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public ICollection<Account> Accounts { get; set; } = new List<Account>();

    public ICollection<CompactorMaster> Compactors { get; set; } = new List<CompactorMaster>();

  }
}
