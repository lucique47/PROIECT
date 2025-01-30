using AplicatieStudenti.Models;

namespace AplicatieStudenti.Models
{
    public class ProfesorCurs
    {
        public int ProfesorId { get; set; }
        public int CursId { get; set; }
        public required Profesor Profesor { get; set; }
        public required Curs Curs { get; set; }
    }

}
