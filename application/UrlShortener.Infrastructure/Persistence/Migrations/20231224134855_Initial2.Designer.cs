﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UrlShortener.Infrastructure.Persistence.EntityFramework;

#nullable disable

namespace UrlShortener.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(UrlShortenerDbContext))]
    [Migration("20231224134855_Initial2")]
    partial class Initial2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("dbo")
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("UrlShortener.Domain.TagDetails.TagDetail", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("id");

                    b.Property<long>("ClickedCount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValue(0L)
                        .HasColumnName("click_count");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("creation_date");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("deleted_date");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("is_deleted");

                    b.Property<DateTime?>("LastCallTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("last_access_time");

                    b.Property<DateTime?>("ModifiedAt")
                        .IsConcurrencyToken()
                        .HasColumnType("datetime2")
                        .HasColumnName("last_modify_date");

                    b.Property<string>("TagId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("TagId")
                        .IsUnique();

                    b.ToTable("tagdetails", "dbo");
                });

            modelBuilder.Entity("UrlShortener.Domain.Tags.Tag", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("creation_date");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("deleted_date");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("is_deleted");

                    b.Property<bool>("IsPublic")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("is_public");

                    b.Property<DateTime?>("ModifiedAt")
                        .IsConcurrencyToken()
                        .HasColumnType("datetime2")
                        .HasColumnName("last_modify_date");

                    b.ComplexProperty<Dictionary<string, object>>("Description", "UrlShortener.Domain.Tags.Tag.Description#Description", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("description");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Ip", "UrlShortener.Domain.Tags.Tag.Ip#Ip", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<int>("Type")
                                .HasColumnType("int")
                                .HasColumnName("ip_address_type");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("ip_address");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("LongUrl", "UrlShortener.Domain.Tags.Tag.LongUrl#LongUrl", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("long_url");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("ShortUrl", "UrlShortener.Domain.Tags.Tag.ShortUrl#ShortUrl", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("short_url");
                        });

                    b.HasKey("Id");

                    b.ToTable("tags", "dbo");
                });

            modelBuilder.Entity("UrlShortener.Domain.TagDetails.TagDetail", b =>
                {
                    b.HasOne("UrlShortener.Domain.Tags.Tag", null)
                        .WithOne("TagDetail")
                        .HasForeignKey("UrlShortener.Domain.TagDetails.TagDetail", "TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UrlShortener.Domain.Tags.Tag", b =>
                {
                    b.Navigation("TagDetail");
                });
#pragma warning restore 612, 618
        }
    }
}
