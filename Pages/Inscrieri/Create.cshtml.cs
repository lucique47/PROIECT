using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AplicatieStudenti.Data;
using AplicatieStudenti.Modele;
using Microsoft.EntityFrameworkCore;

namespace AplicatieStudenti.Pages.Inscrieri
{
    public class CreateModel : PageModel
    {
        private readonly AplicatieStudentiContext _context;

        public CreateModel(AplicatieStudentiContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Inscriere Inscriere { get; set; }

        // Proprietăți pentru listele derulante
        public SelectList StudentiSelectList { get; set; }
        public SelectList CursuriSelectList { get; set; }
        public SelectList ProfesoriSelectList { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // Populează listele derulante cu date din baza de date
            StudentiSelectList = new SelectList(await _context.Studenti.ToListAsync(), "ID", "Nume");
            CursuriSelectList = new SelectList(await _context.Cursuri.ToListAsync(), "ID", "NumeCurs");
            ProfesoriSelectList = new SelectList(await _context.Profesori.ToListAsync(), "ID", "Nume");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Inscriere.Add(Inscriere);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }

}
