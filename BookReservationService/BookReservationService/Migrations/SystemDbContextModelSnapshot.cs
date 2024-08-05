﻿// <auto-generated />
using BookReservationService.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookReservationService.Migrations
{
    [DbContext(typeof(SystemDbContext))]
    partial class SystemDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.7");

            modelBuilder.Entity("BookReservationService.Models.BookReservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BookId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Reserved")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("BookReservations");
                });
#pragma warning restore 612, 618
        }
    }
}