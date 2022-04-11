using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace TopLearn.Core.ViewModels.UserPanel;

public class EditUserProfileViewModel
{
    [Required]
    [MaxLength(100)]
    public string UserName { get; set; }

    [Required]
    [MaxLength(100)]
    [EmailAddress]
    public string Email { get; set; }
}