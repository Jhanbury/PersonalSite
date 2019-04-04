﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Site.Persistance.Migrations
{
    [DbContext(typeof(SiteDbContext))]
    partial class SiteDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Site.Domains.Entities.Address", b =>
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

            modelBuilder.Entity("Site.Domains.Entities.ContactInformation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AddressId");

                    b.Property<string>("Email");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("ContactInformations");
                });

            modelBuilder.Entity("Site.Domains.Entities.GithubRepo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("CreatedAt");

                    b.Property<string>("Description");

                    b.Property<long>("Forks");

                    b.Property<string>("FullName");

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

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("GithubRepos");
                });

            modelBuilder.Entity("Site.Domains.Entities.PersonalProject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("ImageUrl");

                    b.Property<int>("ProjectType");

                    b.Property<string>("ProjectUrl");

                    b.Property<string>("Title");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("PersonalProjects");
                });

            modelBuilder.Entity("Site.Domains.Entities.PersonalProjectTechnology", b =>
                {
                    b.Property<int>("PersonalProjectId");

                    b.Property<int>("TechnologyId");

                    b.HasKey("PersonalProjectId", "TechnologyId");

                    b.HasIndex("TechnologyId");

                    b.ToTable("PersonalProjectTechnologies");
                });

            modelBuilder.Entity("Site.Domains.Entities.SocialMediaAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountUrl");

                    b.Property<int>("Type");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("SocialMediaAccounts");
                });

            modelBuilder.Entity("Site.Domains.Entities.Technology", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImageUrl");

                    b.Property<string>("Name");

                    b.Property<int>("TechnologyType");

                    b.HasKey("Id");

                    b.ToTable("Technologies");
                });

            modelBuilder.Entity("Site.Domains.Entities.User", b =>
                {
                    b.Property<int>("Id");

                    b.Property<int>("ContactInformationId");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Site.Domains.Entities.ContactInformation", b =>
                {
                    b.HasOne("Site.Domains.Entities.Address", "Address")
                        .WithMany("ContactInformations")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Site.Domains.Entities.GithubRepo", b =>
                {
                    b.HasOne("Site.Domains.Entities.User", "User")
                        .WithMany("GithubRepos")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Site.Domains.Entities.PersonalProject", b =>
                {
                    b.HasOne("Site.Domains.Entities.User", "User")
                        .WithMany("PersonalProjects")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Site.Domains.Entities.PersonalProjectTechnology", b =>
                {
                    b.HasOne("Site.Domains.Entities.PersonalProject", "PersonalProject")
                        .WithMany("Technologies")
                        .HasForeignKey("PersonalProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Site.Domains.Entities.Technology", "Technology")
                        .WithMany("PersonalProjects")
                        .HasForeignKey("TechnologyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Site.Domains.Entities.SocialMediaAccount", b =>
                {
                    b.HasOne("Site.Domains.Entities.User", "User")
                        .WithMany("SocialMediaAccounts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Site.Domains.Entities.User", b =>
                {
                    b.HasOne("Site.Domains.Entities.ContactInformation", "ContactInformation")
                        .WithOne("User")
                        .HasForeignKey("Site.Domains.Entities.User", "Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
