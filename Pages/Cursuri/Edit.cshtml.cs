using AplicatieStudenti.Data;
using AplicatieStudenti.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AplicatieStudenti.Pages.Cursuri
{
    public class EditModel(AplicatieStudentiContext context) : PageModel
    {
        private readonly AplicatieStudentiContext _context = context;

        [BindProperty]
        public required Curs Curs { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

#pragma warning disable CS8601 
            Curs = await _context.Cursuri
                .Include(c => c.ProfesorCursuri)
                .Include(c => c.Inscriere)
                .FirstOrDefaultAsync(m => m.ID == id);
#pragma warning restore CS8601 

            if (Curs == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                var cursToUpdate = await _context.Cursuri
                    .Include(c => c.ProfesorCursuri)
                    .Include(c => c.Inscriere)
                    .FirstOrDefaultAsync(c => c.ID == Curs.ID);

                if (cursToUpdate == null)
                {
                    return NotFound();
                }

                cursToUpdate.NumeCurs = Curs.NumeCurs;
                cursToUpdate.Descriere = Curs.Descriere;

                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CursExists(Curs.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        private bool CursExists(int id)
        {
            return _context.Cursuri.Any(e => e.ID == id);
        }
    }
}