using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TitleDeedManagementSystem.Models
{
  public class Account
  {
    [Key]
    public int AccountId { get; set; }

    [Required]
    [StringLength(20)]
    public string AccountNumber { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string CIFNumber { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string AccountHolderName { get; set;  } = string.Empty;

    [StringLength(20)]
    public string? ProductCode { get; set;  }

    [StringLength(200)]
    public string? ProductDescription {  get; set; }

    public decimal LoanLimit { get; set; }

    public decimal OutstandingAmount { get; set; }

    public DateTime? AccountOpenDate { get; set; }

    public int BranchSettingId { get; set; }

    [ForeignKey(nameof(BranchSettingId))]
    public BranchSettings BranchSettings { get; set; } = null;

    public ICollection<Collateral> Collaterals { get; set; } = new List<Collateral>();

    public DateTime CreatedDate {  get; set; }
  }
}
