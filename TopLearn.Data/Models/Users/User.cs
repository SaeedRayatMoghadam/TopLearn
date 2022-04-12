using System.ComponentModel.DataAnnotations;
using TopLearn.Data.Models.Wallets;

namespace TopLearn.Data.Models.Users;

public class User : BaseEntity
{
    public User()
    {
        
    }

    [Required]
    [MaxLength(100)]
    public string UserName { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Password { get; set; }
    
    [Required]
    [MaxLength(100)]
    [EmailAddress]
    public string Email { get; set; }
    
    public string ActiveCode { get; set; }
    public bool IsActive { get; set; }
    public string Avatar { get; set; }


    #region Relations

    public virtual List<UserRole> UserRoles { get; set; }
    public virtual List<Wallet> Wallets { get; set; }

    #endregion
}