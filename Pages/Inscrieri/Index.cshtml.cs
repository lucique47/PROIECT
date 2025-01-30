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
    public class IndexModel(AplicatieStudenti.Data.AplicatieStudentiContext context) : PageModel
    {
        private readonly AplicatieStudenti.Data.AplicatieStudentiContext _context = context;

        public IList<Inscriere> Inscrieri { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Inscrieri = await _context.Inscrieri
                .Include(i => i.Curs)
                .Include(i => i.Profesor)
                .Include(i => i.Student).ToListAsync();
        }
    }
}
