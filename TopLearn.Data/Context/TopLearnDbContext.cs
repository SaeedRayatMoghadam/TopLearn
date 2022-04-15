using Microsoft.EntityFrameworkCore;
using TopLearn.Data.Models.Users;
using TopLearn.Data.Models.Wallets;

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

    #region Wallets

    public DbSet<Wallet> Wallets { get; set; }
    public DbSet<WalletType> WalletTypes { get; set; }

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);

        base.OnModelCreating(modelBuilder);
    }
}