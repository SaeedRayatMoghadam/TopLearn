using Microsoft.EntityFrameworkCore;
using TopLearn.Data.Models.Categories;
using TopLearn.Data.Models.Users;
using TopLearn.Data.Models.Wallets;

namespace TopLearn.Data.Context;

public class TopLearnDbContext : DbContext
{
    public TopLearnDbContext(DbContextOptions<TopLearnDbContext> options) : base(options)
    {

    }


    //Users
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }


    //Wallets
    public DbSet<Wallet> Wallets { get; set; }
    public DbSet<WalletType> WalletTypes { get; set; }
    

    //Categories
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);
        modelBuilder.Entity<Category>().HasQueryFilter(c => !c.IsDeleted);

        base.OnModelCreating(modelBuilder);
    }
}