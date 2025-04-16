using Microsoft.EntityFrameworkCore;
using Backend.Src.Models.UserModel;

namespace Backend.Src.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
}