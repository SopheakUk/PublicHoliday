﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PublicHoliday.Repository;

#nullable disable

namespace PublicHoliday.Migrations
{
    [DbContext(typeof(PublicHolidayRepository))]
    [Migration("20220821154848_1")]
    partial class _1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PublicHoliday.Model.HolidayType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("SupportedCountryId")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SupportedCountryId");

                    b.ToTable("HolidayType");
                });

            modelBuilder.Entity("PublicHoliday.Model.Region", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SupportedCountryId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SupportedCountryId");

                    b.ToTable("Region");
                });

            modelBuilder.Entity("PublicHoliday.Model.SupportedCountry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CountryCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FromDay")
                        .HasColumnType("int");

                    b.Property<int>("FromMonth")
                        .HasColumnType("int");

                    b.Property<int>("FromYear")
                        .HasColumnType("int");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ToDay")
                        .HasColumnType("int");

                    b.Property<int>("ToMonth")
                        .HasColumnType("int");

                    b.Property<int>("ToYear")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("SupportedCountry");
                });

            modelBuilder.Entity("PublicHoliday.Model.HolidayType", b =>
                {
                    b.HasOne("PublicHoliday.Model.SupportedCountry", null)
                        .WithMany("HolidayTypes")
                        .HasForeignKey("SupportedCountryId");
                });

            modelBuilder.Entity("PublicHoliday.Model.Region", b =>
                {
                    b.HasOne("PublicHoliday.Model.SupportedCountry", null)
                        .WithMany("Regions")
                        .HasForeignKey("SupportedCountryId");
                });

            modelBuilder.Entity("PublicHoliday.Model.SupportedCountry", b =>
                {
                    b.Navigation("HolidayTypes");

                    b.Navigation("Regions");
                });
#pragma warning restore 612, 618
        }
    }
}
