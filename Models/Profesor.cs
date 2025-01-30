namespace AplicatieStudenti.Models;

public class Profesor
{
    public Profesor()
    {
        ProfesorCursuri = [];
        CursuriPredate = [];
        Inscriere = [];
    }
    public int ID { get; set; }
    public required string Nume { get; set; }
    public required string Prenume { get; set; }
    public required string Specializare { get; set; }
    public ICollection<Curs> CursuriPredate { get; set; }
    public ICollection<Inscriere> Inscriere { get; set; }
    public ICollection<ProfesorCurs> ProfesorCursuri { get; set; }
}