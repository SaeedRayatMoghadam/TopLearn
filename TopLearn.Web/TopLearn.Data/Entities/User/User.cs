using System.ComponentModel.DataAnnotations;

namespace TopLearn.Data.Entities.User;

public class User
{
    public User()
    {
        
    }

    [Key]
    public int Id { get; set; }

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

    [Display(Name = "کد فعال سازی")]
    public string ActiveCode { get; set; }

    [Display(Name = "وضعیت")]
    public bool IsActive { get; set; }

    [Display(Name = "آواتار")]
    public string Avatar { get; set; }

    [Display(Name = "تاریخ ثبت نام")]
    public DateTime RegisterDate { get; set; }

    #region Relations

    public virtual List<UserRole> UserRoles { get; set; }

    #endregion
}