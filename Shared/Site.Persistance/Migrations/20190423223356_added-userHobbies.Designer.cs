﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Site.Persistance;

namespace Site.Persistance.Migrations
{
    [DbContext(typeof(SiteDbContext))]
    [Migration("20190423223356_added-userHobbies")]
    partial class addeduserHobbies
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Site.Application.Entities.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AddressLine1");

                    b.Property<string>("AddressLine2");

                    b.Property<string>("AddressLine3");

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("Site.Application.Entities.GithubRepo", b =>
                {
                    b.Property<int>("RepoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("CreatedAt");

                    b.Property<string>("Description");

                    b.Property<long>("Forks");

                    b.Property<string>("FullName");

                    b.Property<long>("GithubId");

                    b.Property<bool>("HasDownloads");

                    b.Property<bool>("HasIssues");

                    b.Property<bool>("HasWiki");

                    b.Property<string>("Language");

                    b.Property<string>("Name");

                    b.Property<long>("OpenIssues");

                    b.Property<long>("OpenIssuesCount");

                    b.Property<long>("StargazersCount");

                    b.Property<string>("Url");

                    b.Property<int>("UserId");

                    b.Property<long>("Watchers");

                    b.Property<long>("WatchersCount");

                    b.HasKey("RepoId");

                    b.HasIndex("UserId");

                    b.ToTable("GithubRepos");
                });

            modelBuilder.Entity("Site.Application.Entities.Hobby", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("HobbyTypeId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("HobbyTypeId");

                    b.ToTable("Hobby");
                });

            modelBuilder.Entity("Site.Application.Entities.HobbyType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.ToTable("HobbyType");
                });

            modelBuilder.Entity("Site.Application.Entities.SocialMediaAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountUrl");

                    b.Property<int>("SocialMediaPlatformId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("SocialMediaPlatformId");

                    b.HasIndex("UserId");

                    b.ToTable("SocialMediaAccounts");
                });

            modelBuilder.Entity("Site.Application.Entities.SocialMediaPlatform", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("SocialMediaPlatform");
                });

            modelBuilder.Entity("Site.Application.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AddressId");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("PersonalStatement");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Site.Application.Entities.UserHobby", b =>
                {
                    b.Property<int>("HobbyId");

                    b.Property<int>("UserId");

                    b.HasKey("HobbyId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserHobby");
                });

            modelBuilder.Entity("Site.Application.Entities.GithubRepo", b =>
                {
                    b.HasOne("Site.Application.Entities.User", "User")
                        .WithMany("GithubRepos")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Site.Application.Entities.Hobby", b =>
                {
                    b.HasOne("Site.Application.Entities.HobbyType", "HobbyType")
                        .WithMany("Hobbies")
                        .HasForeignKey("HobbyTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Site.Application.Entities.SocialMediaAccount", b =>
                {
                    b.HasOne("Site.Application.Entities.SocialMediaPlatform", "SocialMediaPlatform")
                        .WithMany("SocialMediaAccounts")
                        .HasForeignKey("SocialMediaPlatformId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Site.Application.Entities.User", "User")
                        .WithMany("SocialMediaAccounts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Site.Application.Entities.User", b =>
                {
                    b.HasOne("Site.Application.Entities.Address", "Address")
                        .WithMany("Users")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Site.Application.Entities.UserHobby", b =>
                {
                    b.HasOne("Site.Application.Entities.Hobby", "Hobby")
                        .WithMany("UserHobbies")
                        .HasForeignKey("HobbyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Site.Application.Entities.User", "User")
                        .WithMany("UserHobbies")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
