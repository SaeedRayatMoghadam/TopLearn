using System.ComponentModel.DataAnnotations;

namespace TopLearn.Core.DTOs.UserPanel;

public class ChangePasswordViewModel
{
    [Display(Name = "رمز عبور فعلی")]    
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد .")]
    public string CurrentPassword { get; set; }

    [Display(Name = "رمز عبور جدید")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد .")]
    public string Password { get; set; }

    [Display(Name = "تایید رمز عبور جدید")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد .")]
    [Compare("Password", ErrorMessage = "رمز های عبور مطابقت ندارند")]
    public string ConfirmPassword { get; set; }
}