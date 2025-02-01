using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AplicatieStudenti.Data;
using AplicatieStudenti.Models;

namespace AplicatieStudenti.Pages.Inscrieri
{
    public class EditModel(AplicatieStudenti.Data.AplicatieStudentiContext context) : PageModel
    {
        private readonly AplicatieStudenti.Data.AplicatieStudentiContext _context = context;

        [BindProperty]
        public required Inscriere Inscriere { get; set; }
        public required SelectList StudentiSelectList { get; set; }
        public required SelectList CursuriSelectList { get; set; }
        public required SelectList ProfesoriSelectList { get; set; }
        // Metoda care obtine detaliile inscrierii pe baza ID-ului
        public async Task<Inscriere?> GetInscriereAsync(int id)
        {
            return await _context.Inscrieri
                        .Include(i => i.Student)
                        .Include(i => i.Curs)
                        .Include(i => i.Profesor)
                        .FirstOrDefaultAsync(m => m.ID == id);
        }
        // Metoda care se apeleaza pentru a incarca pagina cu detaliile inscrierii
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var inscriere = await GetInscriereAsync(id); // Obtine inscrierea pe baza ID-ului

            if (inscriere == null)
            {
                return NotFound();
            }

            Inscriere = inscriere; // Asociaza inscrierea gasita la modelul de pagina

            // Populeaza lista de studenti pentru dropdown
            StudentiSelectList = new SelectList(await _context.Studenti.ToListAsync(), "ID", "Nume");

            // Populeaza lista de cursuri pentru dropdown
            CursuriSelectList = new SelectList(await _context.Cursuri.ToListAsync(), "ID", "NumeCurs");

            // Populeaza lista de profesori pentru dropdown, in functie de cursul selectat
            ProfesoriSelectList = new SelectList(await _context.ProfesorCursuri
                .Where(pc => pc.CursId == Inscriere.CursID) // Selecteaza profesorii asociati cursului
                .Select(pc => new
                {
                    Id = pc.ProfesorId, // ID-ul profesorului
                    NumeComplet = pc.Profesor.Nume + " " + pc.Profesor.Prenume
                })
                .ToListAsync(), "Id", "NumeComplet");

            return Page();
        }
        // Handler AJAX care obtine profesorii pentru un curs selectat
        public async Task<IActionResult> OnGetProfesoriPentruCursAsync(int cursId)
        {
            var profesori = await _context.ProfesorCursuri
               .Where(pc => pc.CursId == cursId) // Selecteaza profesorii asociati cursului
               .Select(pc => new
               {
                   id = pc.ProfesorId,
                   numeComplet = pc.Profesor.Nume + " " + pc.Profesor.Prenume
               })
               .Distinct() // Asigura ca profesorii sunt unici
               .ToListAsync();

            return new JsonResult(profesori);
        }
        // Metoda care se apeleaza pentru a salva modificarile in baza de date
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Inscriere).State = EntityState.Modified; // Marcheaza entitatea ca fiind modificata

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InscriereExists(Inscriere.ID)) // Verifica daca inscrierea mai exista
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }
        // Metoda care verifica daca o inscriere exista in baza de date
        private bool InscriereExists(int id)
        {
            return _context.Inscrieri.Any(e => e.ID == id);
        }
    }
}
