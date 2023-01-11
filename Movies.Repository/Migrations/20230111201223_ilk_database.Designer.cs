﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Movies.Repository;

#nullable disable

namespace Movies.Repository.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230111201223_ilk_database")]
    partial class ilk_database
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Movies.Core.Model.Movie", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<int?>("MovieReviewId")
                        .HasColumnType("int");

                    b.Property<bool>("adult")
                        .HasColumnType("bit");

                    b.Property<string>("backdrop_path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("original_language")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("original_title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("overview")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("popularity")
                        .HasColumnType("float");

                    b.Property<string>("poster_path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("release_date")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("video")
                        .HasColumnType("bit");

                    b.Property<double>("vote_average")
                        .HasColumnType("float");

                    b.Property<int>("vote_count")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("MovieReviewId");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("Movies.Core.Model.MovieReview", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<int>("Note")
                        .HasColumnType("int");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("MovieReviews");
                });

            modelBuilder.Entity("Movies.Core.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Mail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MovieReviewId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("MovieReviewId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Movies.Core.Model.Movie", b =>
                {
                    b.HasOne("Movies.Core.Model.MovieReview", null)
                        .WithMany("Movie")
                        .HasForeignKey("MovieReviewId");
                });

            modelBuilder.Entity("Movies.Core.Model.User", b =>
                {
                    b.HasOne("Movies.Core.Model.MovieReview", null)
                        .WithMany("Users")
                        .HasForeignKey("MovieReviewId");
                });

            modelBuilder.Entity("Movies.Core.Model.MovieReview", b =>
                {
                    b.Navigation("Movie");

                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
