using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using True.Code.ToDoListAPI.Models;

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
        modelBuilder.Entity<Priority>()
            .Property(e => e.Level)
            .ValueGeneratedNever();

        modelBuilder.Entity<Priority>()
            .HasMany(e => e.ToDoItems)
            .WithOne(e => e.Priority)
            .HasForeignKey(e => e.Level)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired(false);

        modelBuilder.Entity<User>()
            .HasMany(e => e.ToDoItems)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired(false);

        modelBuilder.Entity<ToDoItem>()
            .HasOne(e => e.User)
            .WithMany(e => e.ToDoItems)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired(false);

        modelBuilder.Entity<ToDoItem>()
            .HasOne(e => e.Priority)
            .WithMany(e => e.ToDoItems)
            .HasForeignKey(e => e.Level)
            .IsRequired(false);

        modelBuilder.Entity<Priority>()
            .Property(e => e.Level)
            .HasColumnType("tinyint");

        modelBuilder.Entity<ToDoItem>()
            .Property(e => e.Created)
            .HasDefaultValueSql("getdate()");

        modelBuilder.Entity<ToDoItem>()
            .Property(e => e.Level)
            .HasColumnType("tinyint");

        modelBuilder.Entity<ToDoItem>()
            .Property(e => e.Created)
            .HasColumnType("smalldatetime");

        modelBuilder.Entity<ToDoItem>()
            .Property(e => e.IsCompleted)
            .HasDefaultValue(false);

        modelBuilder.Entity<ToDoItem>()
            .Property(e => e.DueDate)
            .HasColumnType("smalldatetime");


        modelBuilder.Entity<Priority>()
            .HasData(
                new { Level = 1 },
                new { Level = 2 },
                new { Level = 3 });

        modelBuilder.Entity<User>()
            .HasData(
                new User { Id = 1, Name = "Peter" },
                new User { Id = 2, Name = "Sarra" },
                new User { Id = 3, Name = "Bony" });


        modelBuilder.Entity<ToDoItem>()
            .HasData(
                new ToDoItem { Id = 1, Level = 1, Title = "Foo", UserId = 1 },
                new ToDoItem { Id = 2, Level = 2, Title = "Bar", UserId = 2 },
                new ToDoItem { Id = 3, Level = 3, Title = "Baz", UserId = 3 }
            );
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            @"Server=(localdb)\mssqllocaldb;Database=ToDoItemDb;Trusted_Connection=True");
    }
}