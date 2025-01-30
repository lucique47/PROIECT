using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using AplicatieStudenti.Data;
using AplicatieStudenti.Models;

namespace AplicatieStudenti.Pages.Profesori
{
    public class CreateModel(AplicatieStudentiContext context, ILogger<CreateModel> logger) : PageModel
    {
        private readonly AplicatieStudentiContext _context = context;
        private readonly ILogger<CreateModel> _logger = logger;

        [BindProperty]
        public Profesor Profesor { get; set; } = default!;

        [BindProperty]
        public List<int> SelectedCursuri { get; set; } = [];

        public SelectList CursuriSelectList { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            // Încărcăm cursurile disponibile pentru select
            var cursuri = await _context.Cursuri.ToListAsync();
            CursuriSelectList = new SelectList(cursuri, "ID", "NumeCurs");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (Profesor == null)
                {
                    _logger.LogWarning("Profesor model is null");
                    ModelState.AddModelError(string.Empty, "Date incomplete pentru profesor.");
                    return Page();
                }

                // Validări explicite
                if (string.IsNullOrWhiteSpace(Profesor.Nume))
                {
                    ModelState.AddModelError("Profesor.Nume", "Numele este obligatoriu.");
                }
                if (string.IsNullOrWhiteSpace(Profesor.Prenume))
                {
                    ModelState.AddModelError("Profesor.Prenume", "Prenumele este obligatoriu.");
                }
                if (string.IsNullOrWhiteSpace(Profesor.Specializare))
                {
                    ModelState.AddModelError("Profesor.Specializare", "Specializarea este obligatorie.");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("ModelState is invalid: {Errors}",
                        string.Join("; ", ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage)));

                    // Reîncărcăm lista de cursuri
                    var cursuri = await _context.Cursuri.ToListAsync();
                    CursuriSelectList = new SelectList(cursuri, "ID", "NumeCurs");

                    return Page();
                }

                // Inițializăm colecțiile
                Profesor.CursuriPredate = [];
                Profesor.Inscriere = [];
                Profesor.ProfesorCursuri = [];

                // Adăugăm relațiile pentru cursurile selectate
                if (SelectedCursuri != null && SelectedCursuri.Count != 0)
                {
                    foreach (var cursId in SelectedCursuri)
                    {
                        Profesor.ProfesorCursuri.Add(item: new ProfesorCurs
                        {
                            CursId = cursId,
                            Profesor = Profesor,
                            Curs = await _context.Cursuri.FindAsync(cursId) ?? throw new InvalidOperationException("Curs not found")
                        });
                    }
                }

                _context.Profesori.Add(Profesor);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Profesor nou adăugat cu ID: {ProfesorId}", Profesor.ID);
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Eroare la adăugarea profesorului");
                ModelState.AddModelError(string.Empty,
                    "A apărut o eroare la salvarea profesorului. Vă rugăm să încercați din nou.");

                // Reîncărcăm lista de cursuri
                var cursuri = await _context.Cursuri.ToListAsync();
                CursuriSelectList = new SelectList(cursuri, "ID", "NumeCurs");

                return Page();
            }
        }
    }
}