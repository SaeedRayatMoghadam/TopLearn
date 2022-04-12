using System.ComponentModel.DataAnnotations;

namespace TopLearn.Core.ViewModels.UserPanel;

public class ChargeWalletViewModel
{
    [Required]
    public int Amount { get; set; }
}