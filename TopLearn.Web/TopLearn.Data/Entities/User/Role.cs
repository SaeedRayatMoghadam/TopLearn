using System.ComponentModel.DataAnnotations;

namespace TopLearn.Data.Entities.User;

public class Role
{
    public Role()
    {
        
    }

    [Key]
    public int Id { get; set; }

    [Display(Name = "عنوان رول")]
    public string Tilte { get; set; }

    #region Relations

    public virtual List<UserRole> UserRoles { get; set; }

    #endregion
}