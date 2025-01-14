namespace AplicatieStudenti.Models
{
    public class Inscriere
    {
        public int ID { get; set; }

        // Chei externe
        public int StudentID { get; set; }
        public Student? Student { get; set; }

        public int CursID { get; set; }
        public Curs? Curs { get; set; }

        public int ProfesorID { get; set; }
        public Profesor? Profesor { get; set; }
    }
}
