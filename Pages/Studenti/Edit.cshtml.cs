using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AplicatieStudenti.Data;
using AplicatieStudenti.Models;

namespace AplicatieStudenti.Pages.Studenti
{
    public class EditModel(AplicatieStudentiContext context) : PageModel
    {
        private readonly AplicatieStudentiContext _context = context;

        [BindProperty]
        public Student Student { get; set; } = default!;
        // Metoda care se executa atunci cand se face un GET pentru a edita detaliile unui student
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            // Se cauta studentul in baza de date folosind id-ul
            Student = await _context.Studenti.FirstOrDefaultAsync(m => m.ID == id) ?? new Student { Nume = string.Empty, Prenume = string.Empty, Email = string.Empty };
            if (Student == null)
            {
                return NotFound();
            }
            return Page();
        }
        // Metoda care se executa atunci cand formularul de editare este trimis
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                _context.Attach(Student).State = EntityState.Modified; // Marcheaza studentul ca fiind modificat
                await _context.SaveChangesAsync(); // Salveaza modificarile in baza de date
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(Student.ID)) // Verifica daca studentul exista in baza de date
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
        // Verifica daca studentul exista in baza de date
        private bool StudentExists(int id)
        {
            return _context.Studenti.Any(e => e.ID == id); // Verifica daca exista un student cu id-ul dat
        }
    }
}
