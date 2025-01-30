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
    public class DetailsModel(AplicatieStudenti.Data.AplicatieStudentiContext context) : PageModel
    {
        private readonly AplicatieStudenti.Data.AplicatieStudentiContext _context = context;

        public Inscriere Inscriere { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inscriere = await _context.Inscrieri
                .Include(i => i.Student)  
                .Include(i => i.Curs)     
                .Include(i => i.Profesor) 
                .FirstOrDefaultAsync(m => m.ID == id);

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
    }
}
