using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AplicatieStudenti.Data;
using AplicatieStudenti.Modele;

namespace AplicatieStudenti.Pages.Cursuri
{
    public class DeleteModel : PageModel
    {
        private readonly AplicatieStudenti.Data.AplicatieStudentiContext _context;

        public DeleteModel(AplicatieStudenti.Data.AplicatieStudentiContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Curs Curs { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curs = await _context.Cursuri.FirstOrDefaultAsync(m => m.ID == id);

            if (curs == null)
            {
                return NotFound();
            }
            else
            {
                Curs = curs;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curs = await _context.Cursuri.FindAsync(id);
            if (curs != null)
            {
                Curs = curs;
                _context.Cursuri.Remove(Curs);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
