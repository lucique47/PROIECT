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

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesor = await _context.Profesori
                .Include(p => p.ProfesorCursuri)
                .ThenInclude(pc => pc.Curs)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (profesor == null)
            {
                return NotFound();
            }

            Profesor = profesor;

            var cursuri = await _context.Cursuri.ToListAsync();
            CursuriSelectList = new SelectList(cursuri, "ID", "NumeCurs");

            SelectedCursuri = Profesor.ProfesorCursuri.Select(pc => pc.CursId).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var profesorToUpdate = await _context.Profesori
                .Include(p => p.ProfesorCursuri)
                .FirstOrDefaultAsync(p => p.ID == Profesor.ID);

            if (profesorToUpdate == null)
            {
                return NotFound();
            }

            profesorToUpdate.Nume = Profesor.Nume;
            profesorToUpdate.Prenume = Profesor.Prenume;
            profesorToUpdate.Specializare = Profesor.Specializare;

            profesorToUpdate.ProfesorCursuri.Clear();

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

        private bool ProfesorExists(int id)
        {
            return _context.Profesori.Any(e => e.ID == id);
        }
    }
}
