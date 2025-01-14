using Microsoft.EntityFrameworkCore;
using AplicatieStudenti.Models;

namespace AplicatieStudenti.Data
{
    public class AplicatieStudentiContext : DbContext
    {
        public AplicatieStudentiContext(DbContextOptions<AplicatieStudentiContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Studenti { get; set; }
        public DbSet<Curs> Cursuri { get; set; }
        public DbSet<Profesor> Profesori { get; set; }
        public DbSet<Inscriere> Inscriere { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relație many-to-many între Studenți și Cursuri
            modelBuilder.Entity<Student>()
                .HasMany(s => s.CursuriInscrise)
                .WithMany(c => c.StudentiInscrisi)
                .UsingEntity(j => j.ToTable("StudentCurs"));

            // Relație many-to-many între Profesori și Cursuri
            modelBuilder.Entity<Profesor>()
                .HasMany(p => p.CursuriPredate)
                .WithMany(c => c.Profesori)
                .UsingEntity(j => j.ToTable("ProfesorCurs"));

            // Relație many-to-many între Studenți, Cursuri și Profesori
            modelBuilder.Entity<Inscriere>()
                .HasOne(i => i.Student)
                .WithMany(s => s.Inscriere)
                .HasForeignKey(i => i.StudentID);

            modelBuilder.Entity<Inscriere>()
                .HasOne(i => i.Curs)
                .WithMany(c => c.Inscriere)
                .HasForeignKey(i => i.CursID);

            modelBuilder.Entity<Inscriere>()
                .HasOne(i => i.Profesor)
                .WithMany(p => p.Inscriere)
                .HasForeignKey(i => i.ProfesorID);
        }
    }
}

