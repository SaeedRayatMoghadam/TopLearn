namespace TopLearn.Data.Models.Users;

public class UserRole
{
    public UserRole()
    {
        
    }

    public int Id { get; set; }
    public int UserId { get; set; }
    public int RoleId { get; set; }


    #region Relations

    public virtual User User { get; set; }
    public virtual Role Role { get; set; }

    #endregion
}