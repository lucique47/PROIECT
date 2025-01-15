using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AplicatieStudenti.Data;
using AplicatieStudenti.Models;
using System.Linq;
using System.Threading.Tasks;

namespace AplicatieStudenti.Pages.Inscrieri
{
    public class CreateModel(AplicatieStudentiContext context) : PageModel
    {
        private readonly AplicatieStudentiContext _context = context;

        [BindProperty]
        public Inscriere Inscriere { get; set; } = new Inscriere();

        // Metoda pentru a prelua datele la deschiderea paginii Create
        public IActionResult OnGet()
        {
            var studenti = _context.Studenti.ToList();
            var cursuri = _context.Cursuri.ToList();
            var profesori = _context.Profesori.ToList();  // Adăugăm profesori pentru SelectList

            if (studenti == null || studenti.Count == 0)
            {
                ModelState.AddModelError(string.Empty, "Nu există studenți disponibili.");
            }

            if (cursuri == null || cursuri.Count == 0)
            {
                ModelState.AddModelError(string.Empty, "Nu există cursuri disponibile.");
            }

            if (profesori == null || profesori.Count == 0)
            {
                ModelState.AddModelError(string.Empty, "Nu există profesori disponibili.");
            }

            // Populăm ViewData cu listele de SelectList
            ViewData["StudentID"] = new SelectList(studenti, "ID", "Nume");
            ViewData["CursID"] = new SelectList(cursuri, "ID", "Titlu");
            ViewData["ProfesorID"] = new SelectList(profesori, "ID", "Nume");

            return Page();
        }


        // Metoda pentru a salva înscrierea la trimiterea formularului
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Dacă formularul nu este valid, populăm din nou SelectList pentru a le arăta în caz de eroare
                ViewData["StudentID"] = new SelectList(_context.Studenti.ToList(), "ID", "Nume");
                ViewData["CursID"] = new SelectList(_context.Cursuri.ToList(), "ID", "Titlu");
                ViewData["ProfesorID"] = new SelectList(_context.Profesori.ToList(), "ID", "Nume");
                return Page(); // Rămâne pe aceeași pagină pentru a corecta erorile
            }

            // Dacă formularul este valid, adăugăm înscrierea în baza de date
            _context.Inscrieri.Add(Inscriere);
            await _context.SaveChangesAsync();

            // Redirecționează utilizatorul la pagina de Index (lista de înscriere)
            return RedirectToPage("./Index");
        }
    }
}
