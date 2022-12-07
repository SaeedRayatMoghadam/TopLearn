using System.ComponentModel.DataAnnotations;

namespace TopLearn.Core.DTOs.Account;

public class LoginDto
{
    [Display(Name = "ایمیل")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد .")]
    [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمیباشد.")]
    public string Email { get; set; }

    [Display(Name = "رمز عبور")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد .")]
    public string Password { get; set; }

    [Display(Name = "مرا بخاطر بسپار")]
    public bool RememberMe { get; set; }
}