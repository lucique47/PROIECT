namespace AplicatieStudenti.Modele
{
    public class Profesor
    {
        public int ID { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Specializare { get; set; }

        // Relația many-to-many cu Cursuri
        public ICollection<Curs> CursuriPredate { get; set; }
        public ICollection<Inscriere> Inscriere { get; set; }  // Relație cu Inscriere
    }
}
