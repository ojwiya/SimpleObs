﻿using Core.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Infrastructure.Database
{
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options)
           : base(options)
        {
        }

        public virtual DbSet<Grade> Classrooms { get; set; }
        public virtual DbSet<Exam> Exams { get; set; }
        public virtual DbSet<Lesson> Lessons { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentExam> StudentExams { get; set; }
        public virtual DbSet<StudentLesson> StudentLessons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region One to Many İlişkiler
            modelBuilder.Entity<Grade>()
                .HasMany(cls => cls.Students)
                .WithOne(st => st.Class);

            #endregion

            #region Many to Many İlişkiler
            modelBuilder.Entity<StudentExam>()
                .HasKey(se => new { se.StudentId, se.ExamId });

            modelBuilder.Entity<StudentExam>()
                .HasOne(se => se.Exam)
                .WithMany(se => se.StudentExams)
                .HasForeignKey(e => e.ExamId);

            modelBuilder.Entity<StudentExam>()
                .HasOne(se => se.Student)
                .WithMany(se => se.StudentExams)
                .HasForeignKey(s => s.StudentId);

            modelBuilder.Entity<StudentLesson>()
               .HasKey(sl => new { sl.LessonId, sl.StudentId });

            modelBuilder.Entity<StudentLesson>()
                .HasOne(sl => sl.Lesson)
                .WithMany(sl => sl.LessonStudents)
                .HasForeignKey(l => l.LessonId);

            modelBuilder.Entity<StudentLesson>()
                .HasOne(sl => sl.Student)
                .WithMany(sl => sl.StudentLessons)
                .HasForeignKey(s => s.StudentId);
            #endregion

        }
    }
}