﻿// <auto-generated />
using System;
using FantasySoccerManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FantasySoccerManagement.Infrastructure.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220224165505_RenameManagersEntity")]
    partial class RenameManagersEntity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FantasySoccerManagement.Core.Aggregate.League", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Leagues", (string)null);
                });

            modelBuilder.Entity("FantasySoccerManagement.Core.Aggregate.Player", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Country")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<int>("PlayerPositionType")
                        .HasColumnType("integer");

                    b.Property<Guid>("TeamId")
                        .HasColumnType("uuid");

                    b.Property<double>("Value")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("Players", (string)null);
                });

            modelBuilder.Entity("FantasySoccerManagement.Core.Aggregate.Team", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Country")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("ManagerId")
                        .HasColumnType("uuid");

                    b.Property<double>("Money")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<double>("TeamValue")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("Teams", (string)null);
                });

            modelBuilder.Entity("FantasySoccerManagement.Core.Aggregate.TeamManager", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<Guid?>("LeagueId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("TeamId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("LeagueId");

                    b.HasIndex("TeamId");

                    b.ToTable("TeamManagers", (string)null);
                });

            modelBuilder.Entity("FantasySoccerManagement.Core.Aggregate.TeamManager", b =>
                {
                    b.HasOne("FantasySoccerManagement.Core.Aggregate.League", null)
                        .WithMany("TeamManagers")
                        .HasForeignKey("LeagueId");

                    b.HasOne("FantasySoccerManagement.Core.Aggregate.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("FantasySoccerManagement.Core.Aggregate.League", b =>
                {
                    b.Navigation("TeamManagers");
                });
#pragma warning restore 612, 618
        }
    }
}
