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
    public class IndexModel(AplicatieStudenti.Data.AplicatieStudentiContext context) : PageModel
    {
        public IList<Curs> Curs { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Curs = await context.Cursuri.ToListAsync();
        }
    }
}
