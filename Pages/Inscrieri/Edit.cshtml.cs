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

        public async Task<Inscriere?> GetInscriereAsync(int id)
        {
            return await _context.Inscrieri
                        .Include(i => i.Student)
                        .Include(i => i.Curs)
                        .Include(i => i.Profesor)
                        .FirstOrDefaultAsync(m => m.ID == id);
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var inscriere = await GetInscriereAsync(id);

            if (inscriere == null)
            {
                return NotFound();
            }

            Inscriere = inscriere;

            
            StudentiSelectList = new SelectList(await _context.Studenti.ToListAsync(), "ID", "Nume");

            
            CursuriSelectList = new SelectList(await _context.Cursuri.ToListAsync(), "ID", "NumeCurs");

            
            ProfesoriSelectList = new SelectList(await _context.ProfesorCursuri
                .Where(pc => pc.CursId == Inscriere.CursID)
                .Select(pc => new
                {
                    Id = pc.ProfesorId,
                    NumeComplet = pc.Profesor.Nume + " " + pc.Profesor.Prenume
                })
                .ToListAsync(), "Id", "NumeComplet");

            return Page();
        }
        public async Task<IActionResult> OnGetProfesoriPentruCursAsync(int cursId)
        {
            var profesori = await _context.ProfesorCursuri
               .Where(pc => pc.CursId == cursId)
               .Select(pc => new
               {
                   id = pc.ProfesorId,
                   numeComplet = pc.Profesor.Nume + " " + pc.Profesor.Prenume
               })
               .Distinct()
               .ToListAsync();

            return new JsonResult(profesori);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Inscriere).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InscriereExists(Inscriere.ID))
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

        private bool InscriereExists(int id)
        {
            return _context.Inscrieri.Any(e => e.ID == id);
        }
    }
}
