﻿// <auto-generated />
using Interview;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace interview.Migrations
{
    [DbContext(typeof(ProjDbContext))]
    [Migration("20190408172841_initial-mg")]
    partial class initialmg
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Interview.Models.Course", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Course");
                });

            modelBuilder.Entity("Interview.Models.Exam", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<int>("PassScorePercentage");

                    b.HasKey("Id");

                    b.ToTable("Exam");
                });

            modelBuilder.Entity("Interview.Models.ExamCourse", b =>
                {
                    b.Property<string>("ExamId");

                    b.Property<string>("CourseId");

                    b.HasKey("ExamId", "CourseId");

                    b.HasIndex("CourseId");

                    b.ToTable("ExamCourse");
                });

            modelBuilder.Entity("Interview.Models.Question", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Answer");

                    b.Property<string>("Body");

                    b.Property<string>("ExamId");

                    b.HasKey("Id");

                    b.HasIndex("ExamId");

                    b.ToTable("Question");
                });

            modelBuilder.Entity("Interview.Models.User", b =>
                {
                    b.Property<string>("Username")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Password");

                    b.Property<string>("Profile");

                    b.Property<int>("UserType");

                    b.HasKey("Username");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Interview.Models.UserCourse", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("CourseId");

                    b.HasKey("UserId", "CourseId");

                    b.HasIndex("CourseId");

                    b.ToTable("UserCourse");
                });

            modelBuilder.Entity("Interview.Models.UserExam", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("ExamId");

                    b.Property<int>("ScorePercentage");

                    b.HasKey("UserId", "ExamId");

                    b.HasIndex("ExamId");

                    b.ToTable("UserExam");
                });

            modelBuilder.Entity("Interview.Models.ExamCourse", b =>
                {
                    b.HasOne("Interview.Models.Course")
                        .WithMany("ExamCourse")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Interview.Models.Exam")
                        .WithMany("ExamCourse")
                        .HasForeignKey("ExamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Interview.Models.Question", b =>
                {
                    b.HasOne("Interview.Models.Exam", "Exam")
                        .WithMany("Questions")
                        .HasForeignKey("ExamId");
                });

            modelBuilder.Entity("Interview.Models.UserCourse", b =>
                {
                    b.HasOne("Interview.Models.Course", "Course")
                        .WithMany("UserCourse")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Interview.Models.User", "User")
                        .WithMany("UserCourse")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Interview.Models.UserExam", b =>
                {
                    b.HasOne("Interview.Models.Exam", "Exam")
                        .WithMany("UserExam")
                        .HasForeignKey("ExamId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Interview.Models.User", "User")
                        .WithMany("UserExam")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
