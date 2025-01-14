namespace AplicatieStudenti.Models
{
    public class Student
    {
        public int ID { get; set; }
        public required string Nume { get; set; }
        public required string Prenume { get; set; }
        public DateTime DataNasterii { get; set; }
        public required string Email { get; set; }

        // Relația many-to-many cu Cursuri
        public ICollection<Curs>? CursuriInscrise { get; set; }
        public ICollection<Inscriere>? Inscriere { get; set; }  // Relație cu Inscriere
    }
}
