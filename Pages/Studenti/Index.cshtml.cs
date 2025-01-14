using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AplicatieStudenti.Data;
using AplicatieStudenti.Models;

namespace AplicatieStudenti.Pages.Studenti
{
    public class IndexModel : PageModel
    {
        private readonly AplicatieStudenti.Data.AplicatieStudentiContext _context;

        public IndexModel(AplicatieStudenti.Data.AplicatieStudentiContext context)
        {
            _context = context;
        }

        public IList<Student> Student { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Student = await _context.Studenti.ToListAsync();
        }
    }
}
