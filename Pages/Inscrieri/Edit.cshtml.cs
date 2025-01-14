using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AplicatieStudenti.Data;
using AplicatieStudenti.Models;

namespace AplicatieStudenti.Pages.Inscrieri
{
    public class EditModel(AplicatieStudenti.Data.AplicatieStudentiContext context) : PageModel
    {
        private readonly AplicatieStudenti.Data.AplicatieStudentiContext _context = context;

        [BindProperty]
        public required Inscriere Inscriere { get; set; }

        // Proprietăți pentru listele derulante
        public required SelectList StudentiSelectList { get; set; }
        public required SelectList CursuriSelectList { get; set; }
        public required SelectList ProfesoriSelectList { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Încarcă Inscrierea din baza de date
#pragma warning disable CS8601 // Possible null reference assignment.
            Inscriere = await _context.Inscriere
                .Include(i => i.Student)
                .Include(i => i.Curs)
                .Include(i => i.Profesor)
                .FirstOrDefaultAsync(m => m.ID == id);
#pragma warning restore CS8601 // Possible null reference assignment.

            if (Inscriere == null)
            {
                return NotFound();
            }

            // Populează listele derulante
            StudentiSelectList = new SelectList(await _context.Studenti.ToListAsync(), "ID", "Nume");
            CursuriSelectList = new SelectList(await _context.Cursuri.ToListAsync(), "ID", "NumeCurs");
            ProfesoriSelectList = new SelectList(await _context.Profesori.ToListAsync(), "ID", "Nume");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Inscriere).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InscriereExists(Inscriere.ID))
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

        private bool InscriereExists(int id)
        {
            return _context.Inscriere.Any(e => e.ID == id);
        }
    }
}
