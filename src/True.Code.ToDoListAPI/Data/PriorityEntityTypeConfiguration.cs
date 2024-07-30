// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace True.Code.ToDoListAPI.Data;

public class PriorityEntityTypeConfiguration : IEntityTypeConfiguration<Priority>
{
    public void Configure(EntityTypeBuilder<Priority> builder)
    {
        builder
            .Property(e => e.Level)
            .ValueGeneratedNever();

        builder
            .HasMany(e => e.ToDoItems)
            .WithOne(e => e.Priority)
            .HasForeignKey(e => e.Level)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired(false);

        builder
            .Property(e => e.Level)
            .HasColumnType("tinyint");

        builder
            .HasData(
                new { Level = 1 },
                new { Level = 2 },
                new { Level = 3 });
    }
}
