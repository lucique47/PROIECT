using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AplicatieStudenti.Data;
using AplicatieStudenti.Models;

namespace AplicatieStudenti.Pages.Cursuri
{
    public class DetailsModel : PageModel
    {
        private readonly AplicatieStudenti.Data.AplicatieStudentiContext _context;

        public DetailsModel(AplicatieStudenti.Data.AplicatieStudentiContext context)
        {
            _context = context;
        }

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
    }
}
