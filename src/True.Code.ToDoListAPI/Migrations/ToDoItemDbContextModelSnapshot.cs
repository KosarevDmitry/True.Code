﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using True.Code.ToDoListAPI.Data;

#nullable disable

namespace True.Code.ToDoListAPI.Migrations
{
    [DbContext(typeof(ToDoItemDbContext))]
    partial class ToDoItemDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("True.Code.ToDoListAPI.Models.Priority", b =>
                {
                    b.Property<byte>("Level")
                        .HasColumnType("tinyint");

                    b.HasKey("Level");

                    b.ToTable("Priorities");
                });

            modelBuilder.Entity("True.Code.ToDoListAPI.Models.ToDoItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smalldatetime")
                        .HasColumnOrder(7)
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnOrder(2);

                    b.Property<DateTime?>("DueDate")
                        .HasColumnType("smalldatetime")
                        .HasColumnOrder(4);

                    b.Property<bool?>("IsCompleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnOrder(3);

                    b.Property<byte?>("Level")
                        .HasColumnType("tinyint")
                        .HasColumnOrder(5);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnOrder(1);

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnOrder(6);

                    b.HasKey("Id");

                    b.HasIndex("Level");

                    b.HasIndex("UserId");

                    b.ToTable("ToDoItems");
                });

            modelBuilder.Entity("True.Code.ToDoListAPI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnOrder(1);

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("True.Code.ToDoListAPI.Models.ToDoItem", b =>
                {
                    b.HasOne("True.Code.ToDoListAPI.Models.Priority", "Priority")
                        .WithMany("ToDoItems")
                        .HasForeignKey("Level");

                    b.HasOne("True.Code.ToDoListAPI.Models.User", "User")
                        .WithMany("ToDoItems")
                        .HasForeignKey("UserId");

                    b.Navigation("Priority");

                    b.Navigation("User");
                });

            modelBuilder.Entity("True.Code.ToDoListAPI.Models.Priority", b =>
                {
                    b.Navigation("ToDoItems");
                });

            modelBuilder.Entity("True.Code.ToDoListAPI.Models.User", b =>
                {
                    b.Navigation("ToDoItems");
                });
#pragma warning restore 612, 618
        }
    }
}
