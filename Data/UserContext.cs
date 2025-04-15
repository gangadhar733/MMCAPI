using System;
using Microsoft.EntityFrameworkCore;
using MMC.Api.Entities;

namespace MMC.Api.Data;

public class UserContext(DbContextOptions<UserContext> options)
 : DbContext(options)
{
    public DbSet<User> Users => Set<User>();

    public DbSet<Payment> Payments => Set<Payment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            new { Id = 1, Name = "Gangadhar" },
            new { Id = 2, Name = "Sachin B" },
            new { Id = 3, Name = "Nav" },
            new { Id = 4, Name = "Sam" },
            new { Id = 5, Name = "Jan" }
        );
    }

}
