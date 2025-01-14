﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AplicatieStudenti.Data;
using AplicatieStudenti.Modele;

namespace AplicatieStudenti.Pages.Cursuri
{
    public class CreateModel : PageModel
    {
        private readonly AplicatieStudenti.Data.AplicatieStudentiContext _context;

        public CreateModel(AplicatieStudenti.Data.AplicatieStudentiContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Curs Curs { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Cursuri.Add(Curs);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
