﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;
using SeekQ.Geo.Api.Data;

namespace SeekQ.Geo.Api.Migrations
{
    [DbContext(typeof(GeoDbContext))]
    partial class GeoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SeekQ.Geo.Api.Models.CityModel", b =>
                {
                    b.Property<string>("CityId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("StateId")
                        .HasColumnName("StateId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CityId");

                    b.HasIndex("StateId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("SeekQ.Geo.Api.Models.ProfileLocationModel", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CityId")
                        .HasColumnName("CityId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Point>("Location")
                        .HasColumnType("geography");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ZipCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("ProfileLocations");
                });

            modelBuilder.Entity("SeekQ.Geo.Api.Models.StateModel", b =>
                {
                    b.Property<string>("StateId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("StateName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("ZipCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StateId");

                    b.ToTable("States");
                });

            modelBuilder.Entity("SeekQ.Geo.Api.Models.CityModel", b =>
                {
                    b.HasOne("SeekQ.Geo.Api.Models.StateModel", "State")
                        .WithMany()
                        .HasForeignKey("StateId");
                });

            modelBuilder.Entity("SeekQ.Geo.Api.Models.ProfileLocationModel", b =>
                {
                    b.HasOne("SeekQ.Geo.Api.Models.CityModel", "City")
                        .WithMany()
                        .HasForeignKey("CityId");
                });
#pragma warning restore 612, 618
        }
    }
}