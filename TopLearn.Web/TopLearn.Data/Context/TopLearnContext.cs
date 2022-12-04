﻿using Microsoft.EntityFrameworkCore;
using TopLearn.Data.Entities.User;

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
}