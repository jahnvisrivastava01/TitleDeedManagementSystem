using System.ComponentModel.DataAnnotations;

namespace TitleDeedManagementSystem.ViewModel
{
  public class LoginViewModel
  {
    [Required(ErrorMessage ="Employee ID is required")]
    public string EmployeeId { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    


  }
}
