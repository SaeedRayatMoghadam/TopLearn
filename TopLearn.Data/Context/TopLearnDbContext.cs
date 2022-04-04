using Microsoft.EntityFrameworkCore;
using TopLearn.Data.Models.Users;

namespace TopLearn.Data.Context;

public class TopLearnDbContext : DbContext
{
    public TopLearnDbContext(DbContextOptions<TopLearnDbContext> options) : base(options)
    {

    }


    #region Users

    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }

    #endregion
}