using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TitleDeedManagementSystem.Models
{
  public class RackMaster
  {
    [Key]
    public int RackId { get; set; }

    public int CompactorId { get; set; }

    [ForeignKey(nameof(CompactorId))]
    public CompactorMaster Compactor { get; set; } = null!;

    [Required]
    [StringLength(50)]
    public string RackNumber { get; set; } = string.Empty;

    public bool IsActive { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }


    public ICollection<TitleDeedEntry>TitleDeedEntries{get; set; } = new List<TitleDeedEntry>();
  }
}
