﻿// <auto-generated />
using System;
using FindCoachApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FindCoachApi.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230212165803_create_requests_table")]
    partial class createrequeststable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("FindCoachApi.Entities.AreaExpertise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("CoachId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CoachId");

                    b.ToTable("AreasExpertise");
                });

            modelBuilder.Entity("FindCoachApi.Entities.Request", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CoachId")
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id", "CoachId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("FindCoachApi.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("UserType").HasValue("user");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("FindCoachApi.Entities.Coach", b =>
                {
                    b.HasBaseType("FindCoachApi.Entities.User");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<decimal>("HourlyRate")
                        .HasColumnType("decimal(65,30)");

                    b.HasDiscriminator().HasValue("coach");
                });

            modelBuilder.Entity("FindCoachApi.Entities.AreaExpertise", b =>
                {
                    b.HasOne("FindCoachApi.Entities.Coach", null)
                        .WithMany("Areas")
                        .HasForeignKey("CoachId");
                });

            modelBuilder.Entity("FindCoachApi.Entities.Coach", b =>
                {
                    b.Navigation("Areas");
                });
#pragma warning restore 612, 618
        }
    }
}
