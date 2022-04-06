using System.ComponentModel.DataAnnotations;

namespace TopLearn.Core.ViewModels.Account;

public class ForgotPasswordViewModel
{
    [Required]
    [MaxLength(100)]
    [EmailAddress]
    public string Email { get; set; }
}