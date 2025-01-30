using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AplicatieStudenti.Data;
using AplicatieStudenti.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace AplicatieStudenti.Pages.Studenti
{
    public class CreateModel(AplicatieStudentiContext context) : PageModel
    {
        private readonly AplicatieStudentiContext _context = context;

        [BindProperty]
        public required Student Student { get; set; }

        public required SelectList CursuriSelectList { get; set; }
        public required SelectList ProfesoriSelectList { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            CursuriSelectList = new SelectList(await _context.Cursuri.ToListAsync(), "ID", "NumeCurs");
            ProfesoriSelectList = new SelectList(await _context.Profesori.ToListAsync(), "ID", "Nume");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                CursuriSelectList = new SelectList(await _context.Cursuri.ToListAsync(), "ID", "NumeCurs");
                ProfesoriSelectList = new SelectList(await _context.Profesori.ToListAsync(), "ID", "Nume");
                return Page();
            }

            _context.Studenti.Add(Student);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
