﻿// <auto-generated />
using System;
using Ludo_API.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Ludo_API.Migrations
{
    [DbContext(typeof(LudoContext))]
    partial class LudoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Ludo_API.GameEngine.Game.MoveAction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DestinationSquareID")
                        .HasColumnType("int");

                    b.Property<int>("DiceRoll")
                        .HasColumnType("int");

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OptionText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int?>("StartSquareID")
                        .HasColumnType("int");

                    b.Property<bool>("ValidMove")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("DestinationSquareID");

                    b.HasIndex("StartSquareID");

                    b.ToTable("MoveActions");
                });

            modelBuilder.Entity("Ludo_API.Models.Gameboard", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CurrentPlayerID")
                        .HasColumnType("int");

                    b.Property<int?>("GameCreatorID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("GameDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("GameId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("GameStartDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("LastPlayerID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("CurrentPlayerID");

                    b.HasIndex("GameCreatorID");

                    b.HasIndex("LastPlayerID");

                    b.ToTable("Gameboards");
                });

            modelBuilder.Entity("Ludo_API.Models.Player", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("GameboardID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("ID");

                    b.HasIndex("GameboardID");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("Ludo_API.Models.Square", b =>
                {
                    b.Property<int>("ID")
                        .HasColumnType("int");

                    b.Property<int>("GameboardId")
                        .HasColumnType("int");

                    b.Property<int?>("TenantID")
                        .HasColumnType("int");

                    b.HasKey("ID", "GameboardId");

                    b.HasIndex("GameboardId");

                    b.HasIndex("TenantID");

                    b.ToTable("Squares");
                });

            modelBuilder.Entity("Ludo_API.Models.SquareTenant", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PieceCount")
                        .HasColumnType("int");

                    b.Property<int?>("PlayerID")
                        .HasColumnType("int");

                    b.Property<int>("SquareIndex")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("PlayerID");

                    b.ToTable("SquareTenant");
                });

            modelBuilder.Entity("Ludo_API.GameEngine.Game.MoveAction", b =>
                {
                    b.HasOne("Ludo_API.Models.SquareTenant", "DestinationSquare")
                        .WithMany()
                        .HasForeignKey("DestinationSquareID");

                    b.HasOne("Ludo_API.Models.SquareTenant", "StartSquare")
                        .WithMany()
                        .HasForeignKey("StartSquareID");

                    b.Navigation("DestinationSquare");

                    b.Navigation("StartSquare");
                });

            modelBuilder.Entity("Ludo_API.Models.Gameboard", b =>
                {
                    b.HasOne("Ludo_API.Models.Player", "CurrentPlayer")
                        .WithMany()
                        .HasForeignKey("CurrentPlayerID");

                    b.HasOne("Ludo_API.Models.Player", "GameCreator")
                        .WithMany()
                        .HasForeignKey("GameCreatorID");

                    b.HasOne("Ludo_API.Models.Player", "LastPlayer")
                        .WithMany()
                        .HasForeignKey("LastPlayerID");

                    b.Navigation("CurrentPlayer");

                    b.Navigation("GameCreator");

                    b.Navigation("LastPlayer");
                });

            modelBuilder.Entity("Ludo_API.Models.Player", b =>
                {
                    b.HasOne("Ludo_API.Models.Gameboard", null)
                        .WithMany("Players")
                        .HasForeignKey("GameboardID");
                });

            modelBuilder.Entity("Ludo_API.Models.Square", b =>
                {
                    b.HasOne("Ludo_API.Models.Gameboard", "Gameboard")
                        .WithMany("Squares")
                        .HasForeignKey("GameboardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ludo_API.Models.SquareTenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantID");

                    b.Navigation("Gameboard");

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("Ludo_API.Models.SquareTenant", b =>
                {
                    b.HasOne("Ludo_API.Models.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerID");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("Ludo_API.Models.Gameboard", b =>
                {
                    b.Navigation("Players");

                    b.Navigation("Squares");
                });
#pragma warning restore 612, 618
        }
    }
}
