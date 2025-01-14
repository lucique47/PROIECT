using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AplicatieStudenti.Data;
using AplicatieStudenti.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace AplicatieStudenti.Pages.Studenti
{
    public class CreateModel : PageModel
    {
        private readonly AplicatieStudentiContext _context;

        public CreateModel(AplicatieStudentiContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Student Student { get; set; }

        // SelectList pentru Cursuri și Profesori
        public SelectList CursuriSelectList { get; set; }
        public SelectList ProfesoriSelectList { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // Populăm dropdown-ul cu cursuri și profesori
            CursuriSelectList = new SelectList(await _context.Cursuri.ToListAsync(), "ID", "NumeCurs");
            ProfesoriSelectList = new SelectList(await _context.Profesori.ToListAsync(), "ID", "Nume");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Dacă modelul nu este valid, repopulăm dropdown-urile și afișăm formularul din nou
                CursuriSelectList = new SelectList(await _context.Cursuri.ToListAsync(), "ID", "NumeCurs");
                ProfesoriSelectList = new SelectList(await _context.Profesori.ToListAsync(), "ID", "Nume");
                return Page();
            }

            // Adăugăm studentul în baza de date
            _context.Studenti.Add(Student);
            await _context.SaveChangesAsync();

            // După ce studentul a fost adăugat, redirecționăm utilizatorul la pagina principală cu lista de studenți
            return RedirectToPage("./Index");
        }
    }
}
