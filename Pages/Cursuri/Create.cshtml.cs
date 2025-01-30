using AplicatieStudenti.Data;
using AplicatieStudenti.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
namespace AplicatieStudenti.Pages.Cursuri
{

    public class CreateModel(AplicatieStudentiContext context) : PageModel
    {
        private readonly AplicatieStudentiContext _context = context;

        public IActionResult OnGet()
        {
            Curs = new Curs
            {
                ProfesorCursuri = [],
                Inscriere = []
            };
            return Page();
        }

        [BindProperty]
        public required Curs Curs { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                Curs.ProfesorCursuri = [];
                Curs.Inscriere = [];

                _context.Cursuri.Add(Curs);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "A aparut o eroare la salvarea cursului: " + ex.Message);
                return Page();
            }
        }
    }
}