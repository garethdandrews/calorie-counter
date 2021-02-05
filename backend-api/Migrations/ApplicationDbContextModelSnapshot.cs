﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using backend_api.Persistence.Contexts;

namespace backend_api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("backend_api.Domain.Models.DiaryEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int>("CalorieTarget")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("TotalCalories")
                        .HasColumnType("integer");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("DiaryEntries");
                });

            modelBuilder.Entity("backend_api.Domain.Models.FoodItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int>("Calories")
                        .HasColumnType("integer");

                    b.Property<int?>("DiaryEntryId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DiaryEntryId");

                    b.HasIndex("UserId");

                    b.ToTable("FoodItems");
                });

            modelBuilder.Entity("backend_api.Domain.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int>("CalorieTarget")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("backend_api.Domain.Models.DiaryEntry", b =>
                {
                    b.HasOne("backend_api.Domain.Models.User", "User")
                        .WithMany("Diary")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("backend_api.Domain.Models.FoodItem", b =>
                {
                    b.HasOne("backend_api.Domain.Models.DiaryEntry", "DiaryEntry")
                        .WithMany("FoodItems")
                        .HasForeignKey("DiaryEntryId");

                    b.HasOne("backend_api.Domain.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("DiaryEntry");

                    b.Navigation("User");
                });

            modelBuilder.Entity("backend_api.Domain.Models.DiaryEntry", b =>
                {
                    b.Navigation("FoodItems");
                });

            modelBuilder.Entity("backend_api.Domain.Models.User", b =>
                {
                    b.Navigation("Diary");
                });
#pragma warning restore 612, 618
        }
    }
}
