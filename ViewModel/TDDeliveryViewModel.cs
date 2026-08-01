using System.ComponentModel.DataAnnotations;


namespace TitleDeedManagementSystem.ViewModel
{
  public class TDDeliveryViewModel
  {
    public int TitleDeedEntryId { get; set; }
    public int CollateralId { get; set; }
    public string CollateralNumber { get; set; } = string.Empty;

    public string TitleDeedNumber { get; set; } = string.Empty;

    public string CERSAIAssetId { get; set; } = string.Empty;

    [Display(Name="TD Delivery Raised Date")]
    [DataType(DataType.Date)]
    public DateTime? TdDeliveryRaisedDate { get; set; }
  }
}
