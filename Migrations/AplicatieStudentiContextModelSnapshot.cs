﻿// <auto-generated />
using System;
using AplicatieStudenti.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AplicatieStudenti.Migrations
{
    [DbContext(typeof(AplicatieStudentiContext))]
    partial class AplicatieStudentiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AplicatieStudenti.Models.Curs", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Descriere")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumeCurs")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Cursuri");
                });

            modelBuilder.Entity("AplicatieStudenti.Models.Inscriere", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("CursID")
                        .HasColumnType("int");

                    b.Property<string>("Prenume")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("ProfesorID")
                        .HasColumnType("int");

                    b.Property<int>("StudentID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("CursID");

                    b.HasIndex("ProfesorID");

                    b.HasIndex("StudentID");

                    b.ToTable("Inscrieri");
                });

            modelBuilder.Entity("AplicatieStudenti.Models.Profesor", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Nume")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prenume")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Specializare")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Profesori");
                });

            modelBuilder.Entity("AplicatieStudenti.Models.ProfesorCurs", b =>
                {
                    b.Property<int>("CursId")
                        .HasColumnType("int");

                    b.Property<int>("ProfesorId")
                        .HasColumnType("int");

                    b.HasKey("CursId", "ProfesorId");

                    b.HasIndex("ProfesorId");

                    b.ToTable("ProfesorCurs", (string)null);
                });

            modelBuilder.Entity("AplicatieStudenti.Models.Student", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("DataNasterii")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nume")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prenume")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Studenti");
                });

            modelBuilder.Entity("CursStudent", b =>
                {
                    b.Property<int>("CursuriInscriseID")
                        .HasColumnType("int");

                    b.Property<int>("StudentiInscrisiID")
                        .HasColumnType("int");

                    b.HasKey("CursuriInscriseID", "StudentiInscrisiID");

                    b.HasIndex("StudentiInscrisiID");

                    b.ToTable("CursStudent");
                });

            modelBuilder.Entity("AplicatieStudenti.Models.Inscriere", b =>
                {
                    b.HasOne("AplicatieStudenti.Models.Curs", "Curs")
                        .WithMany("Inscriere")
                        .HasForeignKey("CursID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AplicatieStudenti.Models.Profesor", "Profesor")
                        .WithMany("Inscriere")
                        .HasForeignKey("ProfesorID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AplicatieStudenti.Models.Student", "Student")
                        .WithMany("Inscriere")
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Curs");

                    b.Navigation("Profesor");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("AplicatieStudenti.Models.ProfesorCurs", b =>
                {
                    b.HasOne("AplicatieStudenti.Models.Curs", "Curs")
                        .WithMany("ProfesorCursuri")
                        .HasForeignKey("CursId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AplicatieStudenti.Models.Profesor", "Profesor")
                        .WithMany("ProfesorCursuri")
                        .HasForeignKey("ProfesorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Curs");

                    b.Navigation("Profesor");
                });

            modelBuilder.Entity("CursStudent", b =>
                {
                    b.HasOne("AplicatieStudenti.Models.Curs", null)
                        .WithMany()
                        .HasForeignKey("CursuriInscriseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AplicatieStudenti.Models.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentiInscrisiID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AplicatieStudenti.Models.Curs", b =>
                {
                    b.Navigation("Inscriere");

                    b.Navigation("ProfesorCursuri");
                });

            modelBuilder.Entity("AplicatieStudenti.Models.Profesor", b =>
                {
                    b.Navigation("Inscriere");

                    b.Navigation("ProfesorCursuri");
                });

            modelBuilder.Entity("AplicatieStudenti.Models.Student", b =>
                {
                    b.Navigation("Inscriere");
                });
#pragma warning restore 612, 618
        }
    }
}
