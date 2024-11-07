﻿// <auto-generated />
using Catalog_DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Catalog_DAL.Migrations
{
    [DbContext(typeof(CatalogContext))]
    partial class CatalogContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Catalog_DAL.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("categories_pkey");

                    b.ToTable("categories", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "vegetables"
                        },
                        new
                        {
                            Id = 2,
                            Name = "vegetables"
                        },
                        new
                        {
                            Id = 3,
                            Name = "vegetables"
                        },
                        new
                        {
                            Id = 4,
                            Name = "fruits"
                        },
                        new
                        {
                            Id = 5,
                            Name = "vegetables"
                        },
                        new
                        {
                            Id = 6,
                            Name = "vegetables"
                        },
                        new
                        {
                            Id = 7,
                            Name = "vegetables"
                        },
                        new
                        {
                            Id = 8,
                            Name = "vegetables"
                        },
                        new
                        {
                            Id = 9,
                            Name = "vegetables"
                        },
                        new
                        {
                            Id = 10,
                            Name = "vegetables"
                        });
                });

            modelBuilder.Entity("Catalog_DAL.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer")
                        .HasColumnName("category_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("name");

                    b.Property<decimal>("Price")
                        .HasPrecision(10, 2)
                        .HasColumnType("numeric(10,2)")
                        .HasColumnName("price");

                    b.HasKey("Id")
                        .HasName("products_pkey");

                    b.HasIndex("CategoryId");

                    b.ToTable("products", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 10,
                            Name = "banana",
                            Price = 43m
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 1,
                            Name = "banana",
                            Price = 70m
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 2,
                            Name = "peach",
                            Price = 59m
                        },
                        new
                        {
                            Id = 4,
                            CategoryId = 1,
                            Name = "apple",
                            Price = 33m
                        },
                        new
                        {
                            Id = 5,
                            CategoryId = 2,
                            Name = "strawberry",
                            Price = 37m
                        },
                        new
                        {
                            Id = 6,
                            CategoryId = 10,
                            Name = "strawberry",
                            Price = 22m
                        },
                        new
                        {
                            Id = 7,
                            CategoryId = 7,
                            Name = "orange",
                            Price = 88m
                        },
                        new
                        {
                            Id = 8,
                            CategoryId = 5,
                            Name = "orange",
                            Price = 67m
                        },
                        new
                        {
                            Id = 9,
                            CategoryId = 5,
                            Name = "orange",
                            Price = 68m
                        },
                        new
                        {
                            Id = 10,
                            CategoryId = 1,
                            Name = "peach",
                            Price = 74m
                        });
                });

            modelBuilder.Entity("Catalog_DAL.Entities.Product", b =>
                {
                    b.HasOne("Catalog_DAL.Entities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("products_category_id_fkey");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Catalog_DAL.Entities.Category", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}