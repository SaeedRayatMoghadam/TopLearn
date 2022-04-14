using System.ComponentModel.DataAnnotations;

namespace TopLearn.Core.ViewModels.AdminPanel;

public class CreateUserViewModel
{
    [Required]
    [MaxLength(100)]
    public string UserName { get; set; }

    [Required]
    [MaxLength(100)]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MaxLength(100)]
    public string Password { get; set; }

}