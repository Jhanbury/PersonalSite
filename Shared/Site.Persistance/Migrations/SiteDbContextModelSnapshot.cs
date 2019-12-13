﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Site.Persistance;

namespace Site.Persistance.Migrations
{
    [DbContext(typeof(SiteDbContext))]
    partial class SiteDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Site.Domain.Entities.Accreditor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("ImageUrl");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Accreditor");
                });

            modelBuilder.Entity("Site.Domain.Entities.Address", b =>
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

            modelBuilder.Entity("Site.Domain.Entities.Certification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccreditorId");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("AccreditorId");

                    b.ToTable("Certification");
                });

            modelBuilder.Entity("Site.Domain.Entities.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LocationId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("Site.Domain.Entities.Degree", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DegreeTypeId");

                    b.Property<string>("Description");

                    b.Property<string>("Title");

                    b.Property<int>("UniversityId");

                    b.HasKey("Id");

                    b.HasIndex("DegreeTypeId");

                    b.HasIndex("UniversityId");

                    b.ToTable("Degrees");
                });

            modelBuilder.Entity("Site.Domain.Entities.DegreeType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("DegreeTypes");
                });

            modelBuilder.Entity("Site.Domain.Entities.GithubRepo", b =>
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

            modelBuilder.Entity("Site.Domain.Entities.Grade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DisplayName");

                    b.Property<string>("FinalGrade");

                    b.HasKey("Id");

                    b.ToTable("Grades");
                });

            modelBuilder.Entity("Site.Domain.Entities.Hobby", b =>
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

            modelBuilder.Entity("Site.Domain.Entities.HobbyType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.ToTable("HobbyType");
                });

            modelBuilder.Entity("Site.Domain.Entities.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CityName");

                    b.Property<string>("CountryName");

                    b.Property<string>("DisplayName");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("Site.Domain.Entities.PlatformAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Followers");

                    b.Property<string>("IconUrl");

                    b.Property<bool>("IsLive");

                    b.Property<string>("Link");

                    b.Property<int>("Platform");

                    b.Property<string>("PlatformId");

                    b.Property<string>("Title");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("PlatformAccounts");
                });

            modelBuilder.Entity("Site.Domain.Entities.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<int?>("GithubRepoId");

                    b.Property<int>("ProjectType");

                    b.Property<string>("ProjectUrl");

                    b.Property<string>("Title");

                    b.Property<int>("UserId");

                    b.Property<int?>("UserId1");

                    b.HasKey("Id");

                    b.HasIndex("GithubRepoId")
                        .IsUnique()
                        .HasFilter("[GithubRepoId] IS NOT NULL");

                    b.HasIndex("UserId");

                    b.HasIndex("UserId1");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Site.Domain.Entities.ProjectSkill", b =>
                {
                    b.Property<int>("ProjectId");

                    b.Property<int>("SkillId");

                    b.Property<int?>("ProjectId1");

                    b.HasKey("ProjectId", "SkillId");

                    b.HasIndex("ProjectId1");

                    b.HasIndex("SkillId");

                    b.ToTable("ProjectSkills");
                });

            modelBuilder.Entity("Site.Domain.Entities.ProjectTechnology", b =>
                {
                    b.Property<int>("ProjectId");

                    b.Property<int>("TechnologyId");

                    b.Property<int?>("ProjectId1");

                    b.Property<int?>("TechnologyId1");

                    b.HasKey("ProjectId", "TechnologyId");

                    b.HasIndex("ProjectId1");

                    b.HasIndex("TechnologyId");

                    b.HasIndex("TechnologyId1");

                    b.ToTable("ProjectTechnologies");
                });

            modelBuilder.Entity("Site.Domain.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("RoleLocationType");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("Site.Domain.Entities.RoleSkill", b =>
                {
                    b.Property<int>("SkillId");

                    b.Property<int>("RoleId");

                    b.HasKey("SkillId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleSkills");
                });

            modelBuilder.Entity("Site.Domain.Entities.RoleTechnology", b =>
                {
                    b.Property<int>("RoleId");

                    b.Property<int>("TechnologyId");

                    b.HasKey("RoleId", "TechnologyId");

                    b.HasIndex("TechnologyId");

                    b.ToTable("RoleTechnologies");
                });

            modelBuilder.Entity("Site.Domain.Entities.Skill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("Site.Domain.Entities.SocialMediaAccount", b =>
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

            modelBuilder.Entity("Site.Domain.Entities.SocialMediaPlatform", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("SocialMediaPlatform");
                });

            modelBuilder.Entity("Site.Domain.Entities.Technology", b =>
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

            modelBuilder.Entity("Site.Domain.Entities.University", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LocationId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("Universities");
                });

            modelBuilder.Entity("Site.Domain.Entities.User", b =>
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

            modelBuilder.Entity("Site.Domain.Entities.UserBlogPost", b =>
                {
                    b.Property<int>("BlogId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImageUrl");

                    b.Property<int>("Source");

                    b.Property<string>("SourceId");

                    b.Property<string>("Teaser");

                    b.Property<string>("Title");

                    b.Property<string>("Url");

                    b.Property<int>("UserId");

                    b.HasKey("BlogId");

                    b.HasIndex("UserId");

                    b.ToTable("UserBlogPosts");
                });

            modelBuilder.Entity("Site.Domain.Entities.UserCertification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CertificationId");

                    b.Property<DateTime>("DateObtained");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("CertificationId");

                    b.HasIndex("UserId");

                    b.ToTable("UserCertification");
                });

            modelBuilder.Entity("Site.Domain.Entities.UserDegree", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DegreeId");

                    b.Property<DateTime>("EndDate");

                    b.Property<int>("GradeId");

                    b.Property<DateTime>("StartDate");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("DegreeId");

                    b.HasIndex("GradeId");

                    b.HasIndex("UserId");

                    b.ToTable("UserDegrees");
                });

            modelBuilder.Entity("Site.Domain.Entities.UserHobby", b =>
                {
                    b.Property<int>("HobbyId");

                    b.Property<int>("UserId");

                    b.HasKey("HobbyId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserHobby");
                });

            modelBuilder.Entity("Site.Domain.Entities.UserWorkExperience", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CompanyId");

                    b.Property<DateTime?>("EndDate");

                    b.Property<int>("RoleId");

                    b.Property<DateTime>("StartDate");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserWorkExperiences");
                });

            modelBuilder.Entity("Site.Domain.Entities.Video", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PlatformAccountId");

                    b.Property<DateTime>("PublishDate");

                    b.Property<string>("SourceId");

                    b.Property<string>("ThumbnailUrl");

                    b.Property<string>("Title");

                    b.Property<string>("Url");

                    b.Property<string>("VideoDuration");

                    b.Property<int>("ViewCount");

                    b.HasKey("Id");

                    b.HasIndex("PlatformAccountId");

                    b.ToTable("Videos");
                });

            modelBuilder.Entity("Site.Domain.Entities.Certification", b =>
                {
                    b.HasOne("Site.Domain.Entities.Accreditor", "Accreditor")
                        .WithMany("Certifications")
                        .HasForeignKey("AccreditorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Site.Domain.Entities.Company", b =>
                {
                    b.HasOne("Site.Domain.Entities.Location", "Location")
                        .WithMany("Companies")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Site.Domain.Entities.Degree", b =>
                {
                    b.HasOne("Site.Domain.Entities.DegreeType", "DegreeType")
                        .WithMany("Degrees")
                        .HasForeignKey("DegreeTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Site.Domain.Entities.University", "University")
                        .WithMany("Degrees")
                        .HasForeignKey("UniversityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Site.Domain.Entities.GithubRepo", b =>
                {
                    b.HasOne("Site.Domain.Entities.User", "User")
                        .WithMany("GithubRepos")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Site.Domain.Entities.Hobby", b =>
                {
                    b.HasOne("Site.Domain.Entities.HobbyType", "HobbyType")
                        .WithMany("Hobbies")
                        .HasForeignKey("HobbyTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Site.Domain.Entities.PlatformAccount", b =>
                {
                    b.HasOne("Site.Domain.Entities.User", "User")
                        .WithMany("PlatformAccounts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Site.Domain.Entities.Project", b =>
                {
                    b.HasOne("Site.Domain.Entities.GithubRepo", "GithubRepo")
                        .WithOne("Project")
                        .HasForeignKey("Site.Domain.Entities.Project", "GithubRepoId");

                    b.HasOne("Site.Domain.Entities.User")
                        .WithMany("Projects")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Site.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId1");
                });

            modelBuilder.Entity("Site.Domain.Entities.ProjectSkill", b =>
                {
                    b.HasOne("Site.Domain.Entities.Project")
                        .WithMany("ProjectSkills")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Site.Domain.Entities.Skill")
                        .WithMany("ProjectSkills")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Site.Domain.Entities.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId1");

                    b.HasOne("Site.Domain.Entities.Skill", "Skill")
                        .WithMany()
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Site.Domain.Entities.ProjectTechnology", b =>
                {
                    b.HasOne("Site.Domain.Entities.Project")
                        .WithMany("ProjectTechnologies")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Site.Domain.Entities.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId1");

                    b.HasOne("Site.Domain.Entities.Technology")
                        .WithMany("ProjectTechnologies")
                        .HasForeignKey("TechnologyId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Site.Domain.Entities.Technology", "Technology")
                        .WithMany()
                        .HasForeignKey("TechnologyId1");
                });

            modelBuilder.Entity("Site.Domain.Entities.RoleSkill", b =>
                {
                    b.HasOne("Site.Domain.Entities.Role", "Role")
                        .WithMany("RoleSkills")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Site.Domain.Entities.Skill", "Skill")
                        .WithMany("RoleSkills")
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Site.Domain.Entities.RoleTechnology", b =>
                {
                    b.HasOne("Site.Domain.Entities.Role", "Role")
                        .WithMany("RoleTechnologies")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Site.Domain.Entities.Technology", "Technology")
                        .WithMany("RoleTechnologies")
                        .HasForeignKey("TechnologyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Site.Domain.Entities.SocialMediaAccount", b =>
                {
                    b.HasOne("Site.Domain.Entities.SocialMediaPlatform", "SocialMediaPlatform")
                        .WithMany("SocialMediaAccounts")
                        .HasForeignKey("SocialMediaPlatformId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Site.Domain.Entities.User", "User")
                        .WithMany("SocialMediaAccounts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Site.Domain.Entities.University", b =>
                {
                    b.HasOne("Site.Domain.Entities.Location", "Location")
                        .WithMany("Universities")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Site.Domain.Entities.User", b =>
                {
                    b.HasOne("Site.Domain.Entities.Address", "Address")
                        .WithMany("Users")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Site.Domain.Entities.UserBlogPost", b =>
                {
                    b.HasOne("Site.Domain.Entities.User", "User")
                        .WithMany("UserBlogPosts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Site.Domain.Entities.UserCertification", b =>
                {
                    b.HasOne("Site.Domain.Entities.Certification", "Certification")
                        .WithMany("UserCertifications")
                        .HasForeignKey("CertificationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Site.Domain.Entities.User", "User")
                        .WithMany("UserCertifications")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Site.Domain.Entities.UserDegree", b =>
                {
                    b.HasOne("Site.Domain.Entities.Degree", "Degree")
                        .WithMany("UserDegrees")
                        .HasForeignKey("DegreeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Site.Domain.Entities.Grade", "Grade")
                        .WithMany("UserDegrees")
                        .HasForeignKey("GradeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Site.Domain.Entities.User", "User")
                        .WithMany("UserDegrees")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Site.Domain.Entities.UserHobby", b =>
                {
                    b.HasOne("Site.Domain.Entities.Hobby", "Hobby")
                        .WithMany("UserHobbies")
                        .HasForeignKey("HobbyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Site.Domain.Entities.User", "User")
                        .WithMany("UserHobbies")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Site.Domain.Entities.UserWorkExperience", b =>
                {
                    b.HasOne("Site.Domain.Entities.Company", "Company")
                        .WithMany("UserWorkExperiences")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Site.Domain.Entities.Role", "Role")
                        .WithMany("UserWorkExperiences")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Site.Domain.Entities.User", "User")
                        .WithMany("UserWorkExperiences")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Site.Domain.Entities.Video", b =>
                {
                    b.HasOne("Site.Domain.Entities.PlatformAccount", "PlatformAccount")
                        .WithMany("Videos")
                        .HasForeignKey("PlatformAccountId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
