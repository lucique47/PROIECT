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
    public class IndexModel(AplicatieStudenti.Data.AplicatieStudentiContext context) : PageModel
    {
        private readonly AplicatieStudenti.Data.AplicatieStudentiContext _context = context;

        public IList<Profesor> Profesor { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Profesor = await _context.Profesori.ToListAsync();
        }
    }
}
