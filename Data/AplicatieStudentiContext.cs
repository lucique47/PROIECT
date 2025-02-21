﻿using Microsoft.EntityFrameworkCore;
using AplicatieStudenti.Models;

namespace AplicatieStudenti.Data
{
    // Clasa care defineste conexiunea la baza de date si entitatile ce vor fi mapate in SQL
    public class AplicatieStudentiContext(DbContextOptions<AplicatieStudentiContext> options) : DbContext(options)
    {

        // Definim entitatile care vor fi transformate in tabele in baza de date
        public DbSet<Inscriere> Inscrieri { get; set; } // Relatie many-to-many
        public DbSet<Student> Studenti { get; set; }
        public DbSet<Curs> Cursuri { get; set; }
        public DbSet<Profesor> Profesori { get; set; }
        public DbSet<ProfesorCurs> ProfesorCursuri { get; set; } // Relatie many-to-many intre Profesori si Cursuri

        // Configuram relatiile dintre entitati in baza de date
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Profesor>()
                .HasMany(p => p.CursuriPredate)
                .WithMany(c => c.Profesori)
                .UsingEntity<ProfesorCurs>(
                    j => j.HasOne(pc => pc.Curs).WithMany(c => c.ProfesorCursuri).HasForeignKey(pc => pc.CursId),
                    j => j.HasOne(pc => pc.Profesor).WithMany(p => p.ProfesorCursuri).HasForeignKey(pc => pc.ProfesorId),
                    j => j.ToTable("ProfesorCurs")
                );

            modelBuilder.Entity<Inscriere>()
                .HasOne(i => i.Student)
                .WithMany(s => s.Inscriere)
                .HasForeignKey(i => i.StudentID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Inscriere>()
                .HasOne(i => i.Curs)
                .WithMany(c => c.Inscriere)
                .HasForeignKey(i => i.CursID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Inscriere>()
                .HasOne(i => i.Profesor)
                .WithMany(p => p.Inscriere)
                .HasForeignKey(i => i.ProfesorID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
