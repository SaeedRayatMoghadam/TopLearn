using System.ComponentModel.DataAnnotations;

namespace TopLearn.Core.DTOs.UserPanel;

public class ChargeWalletViewModel
{
    [Display(Name = "مبلغ")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public int Amount { get; set; }
}