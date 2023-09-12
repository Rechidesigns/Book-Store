﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Test_Project2.Data_Contest;

#nullable disable

namespace Test_Project2.Migrations
{
    [DbContext(typeof(Book_Shop_DataContext))]
    partial class Book_Shop_DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseSerialColumns(modelBuilder);

            modelBuilder.Entity("Test_Project2.Models.Books_Model", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Authors_Name")
                        .HasColumnType("text");

                    b.Property<string>("Books_File")
                        .HasColumnType("text");

                    b.Property<string>("Books_Title")
                        .HasColumnType("text");

                    b.Property<string>("Cover_Image_Url")
                        .HasColumnType("text");

                    b.Property<DateTime>("Created_On")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Published_Date")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Books_Table");
                });

            modelBuilder.Entity("Test_Project2.Models.Cart_Model", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("Book_Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created_On")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("Customer_Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Total_Amount")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Cart_Table");
                });

            modelBuilder.Entity("Test_Project2.Models.Customer_Details", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address_Line_1")
                        .HasColumnType("text");

                    b.Property<string>("Address_Line_2")
                        .HasColumnType("text");

                    b.Property<DateTime>("Created_On")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("First_Name")
                        .HasColumnType("text");

                    b.Property<string>("Last_Name")
                        .HasColumnType("text");

                    b.Property<string>("Phone_Number")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Customer_Details_Table");
                });

            modelBuilder.Entity("Test_Project2.Models.Order_Model", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("Book_Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("Cart_Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created_On")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("Customer_Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Order_Status")
                        .HasColumnType("text");

                    b.Property<string>("Total_Amount")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Order_Table");
                });
#pragma warning restore 612, 618
        }
    }
}
