using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TitleDeedManagementSystem.Helpers.Enums;

namespace TitleDeedManagementSystem.Models
{
  public class Collateral
  {
    [Key]
    public int CollateralId { get; set; }

    public int AccountId { get; set; }

    [ForeignKey(nameof(AccountId))]
    public Account Account { get; set; } = null!;

    [Required]
    [StringLength(30)]
    public string CollateralNumber { get; set; } = string.Empty;

    public bool IsPrimary {  get; set; }

    [StringLength(50)]
    public string? TitleDeedStatus { get; set; }

    public DateTime? CollateralCreationDate { get; set; }

    public decimal OutstandingAmount { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedDate { get; set; }

    
    public TitleDeedEntry? TitleDeedEntry { get; set; }
  }
}
