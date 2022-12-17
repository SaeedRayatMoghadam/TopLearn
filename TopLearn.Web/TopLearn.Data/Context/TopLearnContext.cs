using Microsoft.EntityFrameworkCore;
using TopLearn.Data.Entities.Course;
using TopLearn.Data.Entities.User;
using TopLearn.Data.Entities.Wallet;

namespace TopLearn.Data.Context;

public class TopLearnContext:DbContext
{
    public TopLearnContext(DbContextOptions<TopLearnContext> options):base(options)
    {
        
    }

    #region User

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }

    #endregion

    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<TransactionTypes> TransactionTypes { get; set; }


    #region Course

    public DbSet<Category> Categories { get; set; }

    #endregion


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDelete);

        base.OnModelCreating(modelBuilder);
    }
}