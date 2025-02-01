using AplicatieStudenti.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AplicatieStudenti.Pages.Profesori
{
    public class EditModel(AplicatieStudenti.Data.AplicatieStudentiContext context) : PageModel
    {
        private readonly AplicatieStudenti.Data.AplicatieStudentiContext _context = context;

        [BindProperty]
        public Profesor Profesor { get; set; } = default!;

        [BindProperty]
        public List<int> SelectedCursuri { get; set; } = [];

        public SelectList CursuriSelectList { get; set; } = default!;
        // Se executa atunci cand se face un GET la pagina de editare a profesorului
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesor = await _context.Profesori
                .Include(p => p.ProfesorCursuri) // Include cursurile asociate profesorului
                .ThenInclude(pc => pc.Curs) // Include detaliile cursurilor
                .FirstOrDefaultAsync(m => m.ID == id); // Gaseste profesorul dupa ID

            if (profesor == null)
            {
                return NotFound();
            }

            Profesor = profesor; // Seteaza profesorul pentru pagina
            // Preia lista de cursuri disponibile
            var cursuri = await _context.Cursuri.ToListAsync();
            CursuriSelectList = new SelectList(cursuri, "ID", "NumeCurs");// Creeaza SelectList pentru dropdown
            // Seteaza cursurile deja asociate profesorului
            SelectedCursuri = Profesor.ProfesorCursuri.Select(pc => pc.CursId).ToList();

            return Page();
        }
        // Se executa atunci cand formularul este trimis pentru a actualiza un profesor
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            // Gaseste profesorul care trebuie actualizat
            var profesorToUpdate = await _context.Profesori
                .Include(p => p.ProfesorCursuri)
                .FirstOrDefaultAsync(p => p.ID == Profesor.ID);

            if (profesorToUpdate == null)
            {
                return NotFound();
            }
            // Actualizeaza detaliile profesorului
            profesorToUpdate.Nume = Profesor.Nume;
            profesorToUpdate.Prenume = Profesor.Prenume;
            profesorToUpdate.Specializare = Profesor.Specializare;
            // Curata lista de cursuri asociate
            profesorToUpdate.ProfesorCursuri.Clear();
            // Adauga noile cursuri selectate
            if (SelectedCursuri != null)
            {
                foreach (var cursId in SelectedCursuri)
                {
                    var curs = await _context.Cursuri.FindAsync(cursId);
                    if (curs != null)
                    {
                        profesorToUpdate.ProfesorCursuri.Add(new ProfesorCurs
                        {
                            CursId = cursId,
                            ProfesorId = profesorToUpdate.ID,
                            Profesor = profesorToUpdate,
                            Curs = curs
                        });
                    }
                }
            }
            // Salveaza modificarile in baza de date
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfesorExists(Profesor.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }
        // Verifica daca profesorul exista in baza de date
        private bool ProfesorExists(int id)
        {
            return _context.Profesori.Any(e => e.ID == id);
        }
    }
}
