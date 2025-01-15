using AplicatieStudenti.Models;

namespace AplicatieStudenti.Models
{
    public class Curs
    {
        public int ID { get; set; }
        public string? NumeCurs { get; set; }
        public string? Descriere { get; set; }
        public ICollection<Student>? StudentiInscrisi { get; set; }
        public ICollection<Profesor>? Profesori { get; set; }
        public ICollection<Inscriere>? Inscriere { get; set; }
    }
}
