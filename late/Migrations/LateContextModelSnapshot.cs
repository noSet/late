﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using late.Data;

namespace late.Migrations
{
    [DbContext(typeof(LateContext))]
    partial class LateContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.2-rtm-30932");

            modelBuilder.Entity("late.Data.Catalog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("PRI");

                    b.Property<string>("Title")
                        .HasMaxLength(64);

                    b.Property<string>("Url")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.ToTable("Catalog");
                });

            modelBuilder.Entity("late.Data.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CatalogId");

                    b.Property<string>("Content");

                    b.Property<DateTime>("Time");

                    b.Property<string>("Title")
                        .HasMaxLength(256);

                    b.Property<string>("Url")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("CatalogId");

                    b.ToTable("Post");
                });

            modelBuilder.Entity("late.Data.Post", b =>
                {
                    b.HasOne("late.Data.Catalog", "Catalog")
                        .WithMany("Posts")
                        .HasForeignKey("CatalogId");
                });
#pragma warning restore 612, 618
        }
    }
}
