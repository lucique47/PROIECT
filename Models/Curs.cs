namespace AplicatieStudenti.Models
{
    public class Curs
    {
        public int ID { get; set; }
        public string? NumeCurs { get; set; }
        public string? Descriere { get; set; }

        // Relația many-to-many cu Studenți
        public ICollection<Student>? StudentiInscrisi { get; set; }

        // Relația many-to-many cu Profesori
        public ICollection<Profesor>? Profesori { get; set; }

        // Relație cu Inscriere
        public ICollection<Inscriere>? Inscriere { get; set; }
    }
}
