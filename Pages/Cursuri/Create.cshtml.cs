// Importarea bibliotecilor necesare pentru a interacționa cu baza de date, modelele și paginile Razor
using AplicatieStudenti.Data;
using AplicatieStudenti.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
namespace AplicatieStudenti.Pages.Cursuri
{
    // Clasa pentru pagina care gestioneaza adaugarea unui nou curs
    public class CreateModel(AplicatieStudentiContext context) : PageModel
    {
        private readonly AplicatieStudentiContext _context = context;
        // Metoda care se executa la accesarea paginii prin cererea GET
        public IActionResult OnGet()
        {
            Curs = new Curs
            {
                ProfesorCursuri = [],
                Inscriere = []
            };
            return Page(); // Returneaza pagina pentru a o afisa utilizatorului
        }
        // Leaga proprietatea Curs de datele trimise prin formular
        [BindProperty]
        public required Curs Curs { get; set; }
        // Metoda care se executa la trimiterea formularului prin POST
        public async Task<IActionResult> OnPostAsync()
        {
            try
            { // Verifica daca datele din formular sunt valide
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