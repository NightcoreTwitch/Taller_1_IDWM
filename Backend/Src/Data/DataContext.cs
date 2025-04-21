using Microsoft.EntityFrameworkCore;
using Backend.Src.Models;

namespace Backend.Src.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Adress> Adresses { get; set; }
}