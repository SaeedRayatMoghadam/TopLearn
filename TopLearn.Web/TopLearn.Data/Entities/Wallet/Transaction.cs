using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TopLearn.Data.Entities.Wallet;

public class Transaction
{
    [Key]
    public int Id { get; set; }

    [Display(Name = "نوع تراکنش")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public int TransactionType { get; set; }

    [Display(Name = "کاربر")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public int UserId { get; set; }

    [Display(Name = "مبلغ")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public int Amount { get; set; }

    [Display(Name = "شرح")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد .")]
    public string Description { get; set; }

    [Display(Name = "وضعیت")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public bool IsPayed { get; set; }

    [Display(Name = "تاریخ")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public DateTime CreateDate { get; set; }


    #region Relation

    [ForeignKey("UserId")]
    public User.User User { get; set; }

    [ForeignKey("TransactionType")]
    public TransactionTypes TransactionTypes { get; set; }

    #endregion
}