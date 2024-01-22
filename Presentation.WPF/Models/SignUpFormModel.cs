namespace Presentation.WPF.Models;

public class SignUpFormModel
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string ConfirmPassword { get; set; } = null!;
    public bool AcceptsUserTerms { get; set; }
    public bool AcceptsMarketingTerms { get; set; }

}
