using System.ComponentModel.DataAnnotations;

namespace TopLearn.Core.ViewModels.UserPanel;

public class ChangePasswordViewModel
{
    [Required]
    [MaxLength(100)]
    public string CurrentPassword { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string NewPassword { get; set; }

    [Required]
    [MaxLength(100)]
    [Compare(nameof(NewPassword))]
    public string ConfirmNewPassword { get; set; }
}