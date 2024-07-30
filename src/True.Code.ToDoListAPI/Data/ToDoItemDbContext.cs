using Microsoft.EntityFrameworkCore;

namespace True.Code.ToDoListAPI.Data;

public class ToDoItemDbContext : DbContext
{
    public ToDoItemDbContext(DbContextOptions<ToDoItemDbContext> options)
        : base(options)
    {
    }

    public DbSet<Priority> Priorities { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<ToDoItem> ToDoItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       new UserEntityTypeConfiguration().Configure(modelBuilder.Entity<User>());
       new PriorityEntityTypeConfiguration().Configure(modelBuilder.Entity<Priority>());
       new ToDoItemEntityTypeConfiguration().Configure(modelBuilder.Entity<ToDoItem>());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            @"Server=(localdb)\mssqllocaldb;Database=ToDoItemDb;Trusted_Connection=True");
    }
}
