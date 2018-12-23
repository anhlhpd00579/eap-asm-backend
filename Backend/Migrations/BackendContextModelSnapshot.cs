﻿// <auto-generated />
using System;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Backend.Migrations
{
    [DbContext(typeof(BackendContext))]
    partial class BackendContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Backend.Models.StudentClass", b =>
                {
                    b.Property<string>("StudentClassId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CurrentSubjectId");

                    b.Property<string>("Session");

                    b.Property<DateTime>("StartDate");

                    b.Property<int>("StudentClassStatus");

                    b.Property<int?>("SubjectId");

                    b.HasKey("StudentClassId");

                    b.HasIndex("SubjectId");

                    b.ToTable("StudentClass");
                });

            modelBuilder.Entity("Backend.Models.Subject", b =>
                {
                    b.Property<int>("SubjectId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<int>("SubjectStatus");

                    b.HasKey("SubjectId");

                    b.ToTable("Subject");
                });

            modelBuilder.Entity("Backend.Models.StudentClass", b =>
                {
                    b.HasOne("Backend.Models.Subject", "Subject")
                        .WithMany("StudentClasses")
                        .HasForeignKey("SubjectId");
                });
#pragma warning restore 612, 618
        }
    }
}