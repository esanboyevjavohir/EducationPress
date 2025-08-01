﻿// <auto-generated />
using System;
using EduPress.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EduPress.DataAccess.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("EduPress.Core.Entities.Categories", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("CourseCount")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("EduPress.Core.Entities.ContactLocations", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<decimal>("Latitude")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Longitude")
                        .HasColumnType("numeric");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("ContactLocations");
                });

            modelBuilder.Entity("EduPress.Core.Entities.CourseFaqs", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("CoursesId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("CoursesId");

                    b.ToTable("CourseFaqs");
                });

            modelBuilder.Entity("EduPress.Core.Entities.CourseInstructor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CoursesId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("InstructorsId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("CoursesId");

                    b.HasIndex("InstructorsId");

                    b.ToTable("CourseInstructors");
                });

            modelBuilder.Entity("EduPress.Core.Entities.CourseLessons", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CourseSectionId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<TimeSpan>("DurationMinutes")
                        .HasColumnType("interval");

                    b.Property<bool>("IsFree")
                        .HasColumnType("boolean");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("CourseSectionId");

                    b.ToTable("CourseLessons");
                });

            modelBuilder.Entity("EduPress.Core.Entities.CourseSection", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CoursesId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("DurationMinutes")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TotalLessons")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("CoursesId");

                    b.ToTable("CourseSections");
                });

            modelBuilder.Entity("EduPress.Core.Entities.Courses", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CategoriesId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("DurationMonth")
                        .HasColumnType("integer");

                    b.Property<bool>("IsFree")
                        .HasColumnType("boolean");

                    b.Property<int>("Level")
                        .HasColumnType("integer");

                    b.Property<int>("StudentCount")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TotalLessons")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("CategoriesId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("EduPress.Core.Entities.Enrollment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CoursesId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("EnrollmentDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("EnrollmentStatus")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CoursesId");

                    b.HasIndex("UserId");

                    b.ToTable("Enrollments");
                });

            modelBuilder.Entity("EduPress.Core.Entities.Instructors", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("CourseCount")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("Instructors");
                });

            modelBuilder.Entity("EduPress.Core.Entities.LessonProgress", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CompletedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<decimal>("CompletionPercentage")
                        .HasColumnType("numeric");

                    b.Property<Guid>("CourseLessonsId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CoursesId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("boolean");

                    b.Property<int>("LastPositionSeconds")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CourseLessonsId");

                    b.HasIndex("CoursesId");

                    b.HasIndex("UserId");

                    b.ToTable("LessonProgresses");
                });

            modelBuilder.Entity("EduPress.Core.Entities.OtpCode", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("OtpCodes");
                });

            modelBuilder.Entity("EduPress.Core.Entities.Payment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("EnrollmentId")
                        .HasColumnType("uuid");

                    b.Property<int>("PayStatus")
                        .HasColumnType("integer");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("PaymentType")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("EnrollmentId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("EduPress.Core.Entities.Review", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("CoursesId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("boolean");

                    b.Property<int>("Rating")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CoursesId");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("EduPress.Core.Entities.ReviewReplies", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ReplyText")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ReviewId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ReviewId");

                    b.HasIndex("UserId");

                    b.ToTable("ReviewReplies");
                });

            modelBuilder.Entity("EduPress.Core.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<DateTime?>("RefreshTokenExpireDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ResetPasswordToken")
                        .HasColumnType("text");

                    b.Property<DateTime?>("ResetPasswordTokenExpiry")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c0ae7f44-f3a2-4ea6-8030-01a4ea1b1aee"),
                            CreatedOn = new DateTime(2025, 7, 13, 17, 11, 34, 591, DateTimeKind.Local).AddTicks(9188),
                            Email = "javohir.netdeveloper@gmail.com",
                            FullName = "Esanboyev Javohir",
                            IsActive = true,
                            PasswordHash = "DVa6FkES+3EzHYu0BWRpzIIq1G4DRgus/fx1+VWWSGw=",
                            PhoneNumber = "+998933116612",
                            Role = 1,
                            Salt = "51a3a864-7ed8-4bff-bf5c-1e110ecc6c45"
                        },
                        new
                        {
                            Id = new Guid("f67273d6-d1ee-4129-9740-75a8df1a5c5b"),
                            CreatedOn = new DateTime(2025, 7, 13, 17, 11, 34, 592, DateTimeKind.Local).AddTicks(1431),
                            Email = "biloldeveloper@gmail.com",
                            FullName = "Bilol",
                            IsActive = true,
                            PasswordHash = "stawaDLojBci2JgnmKIpgXbdnDxSqeKaXxv8uQ+2p6o=",
                            PhoneNumber = "+998932884321",
                            Role = 2,
                            Salt = "51a3a864-7ed8-4bff-bf5c-1e110ecc6c45"
                        });
                });

            modelBuilder.Entity("EduPress.Core.Entities.CourseFaqs", b =>
                {
                    b.HasOne("EduPress.Core.Entities.Courses", "Courses")
                        .WithMany("CourseFaqs")
                        .HasForeignKey("CoursesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Courses");
                });

            modelBuilder.Entity("EduPress.Core.Entities.CourseInstructor", b =>
                {
                    b.HasOne("EduPress.Core.Entities.Courses", "Courses")
                        .WithMany("CourseInstructors")
                        .HasForeignKey("CoursesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EduPress.Core.Entities.Instructors", "Instructors")
                        .WithMany("CourseInstructors")
                        .HasForeignKey("InstructorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Courses");

                    b.Navigation("Instructors");
                });

            modelBuilder.Entity("EduPress.Core.Entities.CourseLessons", b =>
                {
                    b.HasOne("EduPress.Core.Entities.CourseSection", "CourseSection")
                        .WithMany("CourseLessons")
                        .HasForeignKey("CourseSectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CourseSection");
                });

            modelBuilder.Entity("EduPress.Core.Entities.CourseSection", b =>
                {
                    b.HasOne("EduPress.Core.Entities.Courses", "Courses")
                        .WithMany("CourseSections")
                        .HasForeignKey("CoursesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Courses");
                });

            modelBuilder.Entity("EduPress.Core.Entities.Courses", b =>
                {
                    b.HasOne("EduPress.Core.Entities.Categories", "Categories")
                        .WithMany("Courses")
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categories");
                });

            modelBuilder.Entity("EduPress.Core.Entities.Enrollment", b =>
                {
                    b.HasOne("EduPress.Core.Entities.Courses", "Courses")
                        .WithMany()
                        .HasForeignKey("CoursesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EduPress.Core.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Courses");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EduPress.Core.Entities.LessonProgress", b =>
                {
                    b.HasOne("EduPress.Core.Entities.CourseLessons", "CourseLessons")
                        .WithMany("LessonProgresses")
                        .HasForeignKey("CourseLessonsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EduPress.Core.Entities.Courses", "Courses")
                        .WithMany()
                        .HasForeignKey("CoursesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EduPress.Core.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CourseLessons");

                    b.Navigation("Courses");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EduPress.Core.Entities.OtpCode", b =>
                {
                    b.HasOne("EduPress.Core.Entities.User", "User")
                        .WithMany("OtpCodes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("EduPress.Core.Entities.Payment", b =>
                {
                    b.HasOne("EduPress.Core.Entities.Enrollment", "Enrollment")
                        .WithMany()
                        .HasForeignKey("EnrollmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Enrollment");
                });

            modelBuilder.Entity("EduPress.Core.Entities.Review", b =>
                {
                    b.HasOne("EduPress.Core.Entities.Courses", "Courses")
                        .WithMany("Reviews")
                        .HasForeignKey("CoursesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EduPress.Core.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Courses");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EduPress.Core.Entities.ReviewReplies", b =>
                {
                    b.HasOne("EduPress.Core.Entities.Review", "Review")
                        .WithMany("ReviewReplies")
                        .HasForeignKey("ReviewId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EduPress.Core.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Review");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EduPress.Core.Entities.Categories", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("EduPress.Core.Entities.CourseLessons", b =>
                {
                    b.Navigation("LessonProgresses");
                });

            modelBuilder.Entity("EduPress.Core.Entities.CourseSection", b =>
                {
                    b.Navigation("CourseLessons");
                });

            modelBuilder.Entity("EduPress.Core.Entities.Courses", b =>
                {
                    b.Navigation("CourseFaqs");

                    b.Navigation("CourseInstructors");

                    b.Navigation("CourseSections");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("EduPress.Core.Entities.Instructors", b =>
                {
                    b.Navigation("CourseInstructors");
                });

            modelBuilder.Entity("EduPress.Core.Entities.Review", b =>
                {
                    b.Navigation("ReviewReplies");
                });

            modelBuilder.Entity("EduPress.Core.Entities.User", b =>
                {
                    b.Navigation("OtpCodes");
                });
#pragma warning restore 612, 618
        }
    }
}
