using System.ComponentModel.DataAnnotations;

namespace TopLearn.Core.ViewModels.Account;

public class ResetPasswordViewModel
{
    public string ActiveCode { get; set; }

    [Required]
    [MaxLength(100)]
    public string Password { get; set; }

    [Required]
    [MaxLength(100)]
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; }
}