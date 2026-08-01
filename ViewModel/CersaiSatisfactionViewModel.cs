using System.ComponentModel.DataAnnotations;



namespace TitleDeedManagementSystem.ViewModel
{
  public class CersaiSatisfactionViewModel
  {
    public int TitleDeedEntryId { get; set; }
    public int CollateralId { get; set; }

    public string CollateralNumber { get; set; } = string.Empty;

    public string TitleDeedNumber { get; set; } = string.Empty;

    public string CERSAIAssetId { get; set; } = string.Empty;

    [Required(ErrorMessage ="Please select the CERSAI Satisfaction Date")]
    [DataType(DataType.Date)]
    public DateTime? CersaiSatisfactionDate { get; set; }
  }
}
