using System.ComponentModel.DataAnnotations;

namespace TopLearn.Web.ViewModels.Account;

public class RegisterViewModel
{
    [Display(Name = "نام کاربری")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد .")]
    public string UserName { get; set; }

    [Display(Name = "ایمیل")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد .")]
    [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمیباشد.")]
    public string Email { get; set; }

    [Display(Name = "رمز عبور")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد .")]
    public string Password { get; set; }
    
    [Display(Name = "تکرار رمز عبور")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد .")]
    [Compare("Password",ErrorMessage = "رمز عبور با تکرار آن مغایرت دارد")]
    public string ConfirmPassword { get; set; }
}