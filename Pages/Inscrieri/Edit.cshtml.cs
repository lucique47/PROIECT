using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AplicatieStudenti.Data;
using AplicatieStudenti.Modele;

namespace AplicatieStudenti.Pages.Inscrieri
{
    public class EditModel : PageModel
    {
        private readonly AplicatieStudenti.Data.AplicatieStudentiContext _context;

        public EditModel(AplicatieStudenti.Data.AplicatieStudentiContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Inscriere Inscriere { get; set; }

        // Proprietăți pentru listele derulante
        public SelectList StudentiSelectList { get; set; }
        public SelectList CursuriSelectList { get; set; }
        public SelectList ProfesoriSelectList { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Încarcă Inscrierea din baza de date
            Inscriere = await _context.Inscriere
                .Include(i => i.Student)
                .Include(i => i.Curs)
                .Include(i => i.Profesor)
                .FirstOrDefaultAsync(m => m.ID == id);

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
