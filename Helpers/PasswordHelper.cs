using Microsoft.AspNetCore.Identity;


namespace TitleDeedManagementSystem.Helpers
{
  public class PasswordHelper
  {
    private readonly PasswordHasher<string> _passwordHasher = new();
    public string HashPassword(string password)
    {
      return _passwordHasher.HashPassword(null, password);
    }

    public bool VerifyPassword(string hashedPassword, string enteredPassword)
    {
      var result = _passwordHasher.VerifyHashedPassword(null, hashedPassword, enteredPassword);
      return result == PasswordVerificationResult.Success;
    }
  }
}
