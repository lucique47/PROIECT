using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AplicatieStudenti.Data;
using AplicatieStudenti.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AplicatieStudenti.Pages.Inscrieri
{
    public class CreateModel(AplicatieStudentiContext context) : PageModel
    {
        private readonly AplicatieStudentiContext _context = context;

        [BindProperty]
        public required Inscriere Inscriere { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // Populăm lista de studenți cu Nume și Prenume
            ViewData["StudentID"] = new SelectList(await _context.Studenti
                .Select(s => new { s.ID, NumeComplet = s.Nume + " " + s.Prenume })
                .ToListAsync(), "ID", "NumeComplet");

            // Populăm lista de cursuri cu Numele cursului
            ViewData["CursID"] = new SelectList(await _context.Cursuri
                .Select(c => new { c.ID, c.NumeCurs })
                .ToListAsync(), "ID", "NumeCurs");

            // Populăm lista de profesori cu Numele complet
            ViewData["ProfesorID"] = new SelectList(await _context.Profesori
                .Select(p => new { p.ID, NumeComplet = p.Nume + " " + p.Prenume })
                .ToListAsync(), "ID", "NumeComplet");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Inscrieri.Add(Inscriere);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
