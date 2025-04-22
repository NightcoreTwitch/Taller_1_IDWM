using Microsoft.EntityFrameworkCore;
using Backend.Src.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Backend.Src.Data;

public class DataContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public DbSet<Adress> Adresses { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<IdentityRole<int>>().HasData(
            new IdentityRole<int> { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole<int> { Id = 2, Name = "User", NormalizedName = "USER" }
        );
    }
}