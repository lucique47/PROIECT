using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AplicatieStudenti.Data;
using AplicatieStudenti.Models;

namespace AplicatieStudenti.Pages.Profesori
{
    public class DetailsModel(AplicatieStudentiContext context) : PageModel
    {
        private readonly AplicatieStudentiContext _context = context;

        public Profesor Profesor { get; set; } = default!;
        public List<Curs> Cursuri { get; set; } = [];

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesor = await _context.Profesori.FirstOrDefaultAsync(m => m.ID == id);
            if (profesor == null)
            {
                return NotFound();
            }
            else
            {
                Profesor = profesor;
                Cursuri = await _context.ProfesorCursuri
                    .Where(pc => pc.ProfesorId == profesor.ID)
                    .Select(pc => pc.Curs)
                    .ToListAsync();
            }
            return Page();
        }
    }
}
