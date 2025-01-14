namespace AplicatieStudenti.Modele
{
    public class Student
    {
        public int ID { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public DateTime DataNasterii { get; set; }
        public string Email { get; set; }

        // Relația many-to-many cu Cursuri
        public ICollection<Curs> CursuriInscrise { get; set; }
        public ICollection<Inscriere> Inscriere { get; set; }  // Relație cu Inscriere
    }
}
