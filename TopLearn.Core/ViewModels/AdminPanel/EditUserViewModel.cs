using System.ComponentModel.DataAnnotations;

namespace TopLearn.Core.ViewModels.AdminPanel;

public class EditUserViewModel
{
    public int Id { get; set; }
    public string UserName { get; set; }
    
    [Required]
    [MaxLength(100)]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Password { get; set; }
    
    public List<int> Roles { get; set; }
}