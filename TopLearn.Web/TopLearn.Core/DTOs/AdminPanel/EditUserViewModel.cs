using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace TopLearn.Core.DTOs.AdminPanel;

public class EditUserViewModel
{
    public int UserId { get; set; }
    public string UserName { get; set; }

    [Display(Name = "ایمیل")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
    [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
    public string Email { get; set; }

    [Display(Name = "کلمه عبور")]
    [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
    public string Password { get; set; }
    
    public IFormFile Image { get; set; }
    public string CurrentImage { get; set; }
    public List<int> Roles { get; set; }
}