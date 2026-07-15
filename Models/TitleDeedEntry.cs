using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using TitleDeedManagementSystem.Helpers.Enums;
namespace TitleDeedManagementSystem.Models
{
  public class TitleDeedEntry
  {
    [Key]
    public int TitleDeedEntryId { get; set; }

    public int CollateralId { get; set; }

    [ForeignKey(nameof(CollateralId))]
    [ValidateNever]
    public Collateral Collateral { get; set; } = null!;
    public bool IsTitleDeedAvailable { get; set; }

    [Required]
    [StringLength(100)]
    public string TitleDeedNumber { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string EMRegisterNumber { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string EMFolioNumber { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string FileNumber { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string CERSAIAssetId { get; set; } = string.Empty;

    public int CompactorId { get; set; }

    [ForeignKey(nameof(CompactorId))]
    [ValidateNever]
    public CompactorMaster Compactor { get; set; } = null!;

    public int RackId { get; set; } 

    [ForeignKey(nameof(RackId))]
    [ValidateNever]
    public RackMaster Rack { get; set; } = null!;

    public int CreatedBy { get; set; }

    public int? LastModifiedBy { get; set; }

    
  

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }
    public TitledeedStatus TitledeedStatus { get; set; }



  }
}
