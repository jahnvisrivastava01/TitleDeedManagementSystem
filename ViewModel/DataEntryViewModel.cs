using TitleDeedManagementSystem.Models;

namespace TitleDeedManagementSystem.ViewModel
{
  public class DataEntryViewModel
  {
    public string AccountNumber { get; set; } = string.Empty;
    public int CollateralId { get; set; }

    public string CIFNumber {  get; set; } = string.Empty;

    public string AccountHolderName {  get; set; } = string.Empty;

    public string ProductCode {  get; set; } = string.Empty;

    public decimal LoanLimit { get; set; }
    public decimal OutstandingAmount { get; set; }

    public DateTime AccountOpenDate { get; set; }

    public List<Collateral> Collaterals { get; set; } = new();
  }
}
