using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TopLearn.Data.Models.Wallets;

public class WalletType
{
    public WalletType()
    {
        
    }

    [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }


    #region Relation

    public virtual List<Wallet> Wallets { get; set; }

    #endregion
}