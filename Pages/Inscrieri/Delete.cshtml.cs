using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AplicatieStudenti.Data;
using AplicatieStudenti.Models;

namespace AplicatieStudenti.Pages.Inscrieri
{
    public class DeleteModel : PageModel
    {
        private readonly AplicatieStudenti.Data.AplicatieStudentiContext _context;

        public DeleteModel(AplicatieStudenti.Data.AplicatieStudentiContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Inscriere Inscriere { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inscriere = await _context.Inscrieri.FirstOrDefaultAsync(m => m.ID == id);

            if (inscriere == null)
            {
                return NotFound();
            }
            else
            {
                Inscriere = inscriere;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inscriere = await _context.Inscrieri.FindAsync(id);
            if (inscriere != null)
            {
                Inscriere = inscriere;
                _context.Inscrieri.Remove(Inscriere);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
