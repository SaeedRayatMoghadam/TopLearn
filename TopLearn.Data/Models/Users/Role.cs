using System.ComponentModel.DataAnnotations;

namespace TopLearn.Data.Models.Users;

public class Role
{
    public Role()
    {
        
    }

    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Title { get; set; }


    #region Relations

    public virtual List<UserRole> UsersRoles { get; set; }

    #endregion
}