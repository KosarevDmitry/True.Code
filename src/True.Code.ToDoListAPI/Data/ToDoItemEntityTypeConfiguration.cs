// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace True.Code.ToDoListAPI.Data;

public class ToDoItemEntityTypeConfiguration : IEntityTypeConfiguration<ToDoItem>
{
    public void Configure(EntityTypeBuilder<ToDoItem> builder)
    {
        builder
            .HasOne(e => e.User)
            .WithMany(e => e.ToDoItems)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired(false);

        builder
            .HasOne(e => e.Priority)
            .WithMany(e => e.ToDoItems)
            .HasForeignKey(e => e.Level)
            .IsRequired(false);


        builder
            .Property(e => e.Created)
            .HasDefaultValueSql("getdate()");

        builder
            .Property(e => e.Level)
            .HasColumnType("tinyint");

        builder
            .Property(e => e.Created)
            .HasColumnType("smalldatetime");

        builder
            .Property(e => e.IsCompleted)
            .HasDefaultValue(false);

        builder
            .Property(e => e.DueDate)
            .HasColumnType("smalldatetime");

        builder
            .HasData(
                new ToDoItem { Id = 1, Level = 1, Title = "Foo", UserId = 1 },
                new ToDoItem { Id = 2, Level = 2, Title = "Bar", UserId = 2 },
                new ToDoItem { Id = 3, Level = 3, Title = "Baz", UserId = 3 }
            );
    }
}
