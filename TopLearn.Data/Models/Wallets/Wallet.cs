using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TopLearn.Data.Models.Users;

namespace TopLearn.Data.Models.Wallets;

public class Wallet
{
    public Wallet()
    {
        
    }

    [Key]
    public int Id { get; set; }

    [Required]
    public int TypeId { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public int Amount { get; set; }

    [MaxLength(500)]
    public string Description { get; set; }

    public bool IsPaid { get; set; }
    public DateTime CreateDate { get; set; }


    #region Relations

    public virtual User User { get; set; }
    [ForeignKey("TypeId")]
    public virtual WalletType WalletType { get; set; }

    #endregion
}