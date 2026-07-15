using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using TitleDeedManagementSystem.Models;

namespace TitleDeedManagementSystem.ViewModel
{
  public class TitleDeedEntryViewModel
  {

    public int AccountId { get; set; }

    public int CollateralId { get; set; }


    public string CollateralNumber { get; set; } = string.Empty;

    [Required(ErrorMessage ="Title Deed Number is required")]

    public string? TitleDeedNumber { get; set; }


    [Required(ErrorMessage = "EM Register Number is required")]
    public string? EMRegisterNumber { get; set; }

    [Required(ErrorMessage = "EM Folio Number is required")]
    public string? EMFolioNumber { get; set; }

    [Required(ErrorMessage = "EM File Number is required")]
    public string? FileNumber { get; set; }

    [Required(ErrorMessage = "CERSAI Asset Id is required")]
    public string? CERSAIAssetId { get; set; }


    [Required(ErrorMessage ="Please select a compactor")]
    public int? CompactorId { get; set; }

    [Required(ErrorMessage = "Please select a rack")]
    public int? RackId { get; set; }


    public bool TitleDeedAvailable { get; set; }

    public IEnumerable<SelectListItem>? Compactors { get; set; }
  }
}

