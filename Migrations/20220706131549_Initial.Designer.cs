﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using thegioididong.Models;

#nullable disable

namespace thegioididong.Migrations
{
    [DbContext(typeof(MyDBConext))]
    [Migration("20220706131549_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("thegioididong.Models.Category", b =>
                {
                    b.Property<int>("Id_Category")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_Category"), 1L, 1);

                    b.Property<string>("Name_Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_Category");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("thegioididong.Models.Comment", b =>
                {
                    b.Property<int>("Id_Com")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_Com"), 1L, 1);

                    b.Property<int>("Id_pro")
                        .HasColumnType("int");

                    b.Property<string>("Name_cus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("mail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_Com");

                    b.HasIndex("Id_pro");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("thegioididong.Models.Image", b =>
                {
                    b.Property<int>("Id_image")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_image"), 1L, 1);

                    b.Property<int>("Id_pro")
                        .HasColumnType("int");

                    b.Property<string>("link_image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_image");

                    b.HasIndex("Id_pro");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("thegioididong.Models.Manufacturer", b =>
                {
                    b.Property<int>("Id_manufacturer")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_manufacturer"), 1L, 1);

                    b.Property<string>("Name_manufacturer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_manufacturer");

                    b.ToTable("Manufacturers");
                });

            modelBuilder.Entity("thegioididong.Models.Order", b =>
                {
                    b.Property<int>("Id_Order")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_Order"), 1L, 1);

                    b.Property<string>("Name_Cus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_Order");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("thegioididong.Models.Order_Detail", b =>
                {
                    b.Property<int>("Id_D_Order")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_D_Order"), 1L, 1);

                    b.Property<int>("Id_Order")
                        .HasColumnType("int");

                    b.Property<int>("Id_pro")
                        .HasColumnType("int");

                    b.Property<int>("amount")
                        .HasColumnType("int");

                    b.HasKey("Id_D_Order");

                    b.HasIndex("Id_Order");

                    b.HasIndex("Id_pro");

                    b.ToTable("Order_Detail");
                });

            modelBuilder.Entity("thegioididong.Models.Product", b =>
                {
                    b.Property<int>("Id_pro")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_pro"), 1L, 1);

                    b.Property<string>("Des")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Id_Category")
                        .HasColumnType("int");

                    b.Property<int>("Id_manufacturer")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Pro_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Ram")
                        .HasColumnType("int");

                    b.Property<int>("Rom")
                        .HasColumnType("int");

                    b.HasKey("Id_pro");

                    b.HasIndex("Id_Category");

                    b.HasIndex("Id_manufacturer");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("thegioididong.Models.Comment", b =>
                {
                    b.HasOne("thegioididong.Models.Product", "Product")
                        .WithMany("Comments")
                        .HasForeignKey("Id_pro")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("thegioididong.Models.Image", b =>
                {
                    b.HasOne("thegioididong.Models.Product", "Product")
                        .WithMany("Images")
                        .HasForeignKey("Id_pro")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("thegioididong.Models.Order_Detail", b =>
                {
                    b.HasOne("thegioididong.Models.Order", "Order")
                        .WithMany("Details")
                        .HasForeignKey("Id_Order")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("thegioididong.Models.Product", "Product")
                        .WithMany("Order_Detail")
                        .HasForeignKey("Id_pro")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("thegioididong.Models.Product", b =>
                {
                    b.HasOne("thegioididong.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("Id_Category")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("thegioididong.Models.Manufacturer", "Manufacturer")
                        .WithMany("Products")
                        .HasForeignKey("Id_manufacturer")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Manufacturer");
                });

            modelBuilder.Entity("thegioididong.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("thegioididong.Models.Manufacturer", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("thegioididong.Models.Order", b =>
                {
                    b.Navigation("Details");
                });

            modelBuilder.Entity("thegioididong.Models.Product", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Images");

                    b.Navigation("Order_Detail");
                });
#pragma warning restore 612, 618
        }
    }
}
