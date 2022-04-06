using System.ComponentModel.DataAnnotations;

namespace TopLearn.Core.ViewModels.Account;

public class LoginViewModel
{
    [Required]
    [MaxLength(100)]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MaxLength(100)]
    public string Password { get; set; }


    public bool RememberMe { get; set; }
}