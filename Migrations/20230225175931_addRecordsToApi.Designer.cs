﻿// <auto-generated />
using ConsoleAppWritesonic.Classes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ConsoleAppWritesonic.Migrations
{
    [DbContext(typeof(ChatDBContext))]
    [Migration("20230225175931_addRecordsToApi")]
    partial class addRecordsToApi
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.3");

            modelBuilder.Entity("ConsoleAppWritesonic.Classes.ApiKey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Account")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ApiKeys");
                });

            modelBuilder.Entity("ConsoleAppWritesonic.Classes.TelegramMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsSent")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TelegramUserId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("TelegramUserId");

                    b.ToTable("TelegramMessages");
                });

            modelBuilder.Entity("ConsoleAppWritesonic.Classes.TelegramUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("EnableGoogleResults")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("EnableMemory")
                        .HasColumnType("INTEGER");

                    b.Property<long>("TelegramUserId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("TelegramUsers");
                });

            modelBuilder.Entity("ConsoleAppWritesonic.Classes.TelegramMessage", b =>
                {
                    b.HasOne("ConsoleAppWritesonic.Classes.TelegramUser", "TelegramUser")
                        .WithMany()
                        .HasForeignKey("TelegramUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TelegramUser");
                });
#pragma warning restore 612, 618
        }
    }
}
