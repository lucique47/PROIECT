using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AplicatieStudenti.Data;
using AplicatieStudenti.Models;

namespace AplicatieStudenti.Pages.Inscrieri
{
    public class CreateModel(AplicatieStudentiContext context) : PageModel
    {
        private readonly AplicatieStudentiContext _context = context;

        [BindProperty]
        public Inscriere Inscriere { get; set; } = new Inscriere();
        // Listele de cursuri si profesori vor fi populate din baza de date
        public List<SelectListItem> CursList { get; set; } = [];
        public List<SelectListItem> ProfesorList { get; set; } = [];
        // Metoda care se executa atunci cand pagina este accesata (GET request)
        public async Task<IActionResult> OnGetAsync()
        {
            // Populeaza lista de studenti din baza de date si o trimite catre ViewData pentru a fi utilizata in formular
            ViewData["StudentList"] = await _context.Studenti
                .Select(s => new SelectListItem
                {
                    Value = s.ID.ToString(),
                    Text = $"{s.Nume} {s.Prenume}"
                })
                .ToListAsync();
            // Populeaza lista de cursuri din baza de date si o trimite catre ViewData
            CursList = await _context.Cursuri
                .Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.NumeCurs
                })
                .ToListAsync();
            // Daca exista un CursID selectat, se populeaza lista de profesori asociati cu acest curs
            if (Inscriere.CursID != 0)
            {
                ProfesorList = await _context.ProfesorCursuri
                    .Where(pc => pc.CursId == Inscriere.CursID)
                    .Select(pc => new SelectListItem
                    {
                        Value = pc.ProfesorId.ToString(),
                        Text = pc.Profesor.Nume + " " + pc.Profesor.Prenume
                    })
                    .ToListAsync();
            }
            else
            {
                ProfesorList = [];
            }

            return Page();
        }
        // Aceasta metoda este apelata atunci cand se face un request AJAX pentru a adauga profesori in functie de curs
        public async Task<IActionResult> OnGetProfesoriPentruCursAsync(int cursId)
        {
            // Selecteaza toti profesorii asociati cu cursul selectat si returneaza datele in format JSON
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
        // Metoda care se executa atunci cand formularul este trimis (POST request)
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["StudentList"] = await _context.Studenti
                    .Select(s => new SelectListItem
                    {
                        Value = s.ID.ToString(),
                        Text = $"{s.Nume} {s.Prenume}"
                    })
                    .ToListAsync();

                CursList = await _context.Cursuri
                    .Select(c => new SelectListItem
                    {
                        Value = c.ID.ToString(),
                        Text = c.NumeCurs
                    })
                    .ToListAsync();
                // Daca cursul este selectat, populeaza lista de profesori pentru acest curs
                if (Inscriere.CursID != 0)
                {
                    ProfesorList = await _context.ProfesorCursuri
                        .Where(pc => pc.CursId == Inscriere.CursID)
                        .Select(pc => new SelectListItem
                        {
                            Value = pc.ProfesorId.ToString(),
                            Text = pc.Profesor.Nume + " " + pc.Profesor.Prenume
                        })
                        .ToListAsync();
                }

                return Page();
            }
            // Verifica daca studentul este deja inscris la acest curs cu acelasi profesor
            bool alreadyEnrolled = await _context.Inscrieri
                .AnyAsync(i => i.StudentID == Inscriere.StudentID &&
                               i.CursID == Inscriere.CursID &&
                               i.ProfesorID == Inscriere.ProfesorID);

            if (alreadyEnrolled)
            {
                TempData["ErrorMessage"] = "Studentul este deja inscris la acest curs cu acest profesor";
                return RedirectToPage("./Create");
            }

            bool profesorValid = await _context.ProfesorCursuri
                .AnyAsync(pc => pc.ProfesorId == Inscriere.ProfesorID && pc.CursId == Inscriere.CursID);

            if (!profesorValid)
            {
                ModelState.AddModelError(string.Empty, "Profesorul selectat nu este asociat cu acest curs");
                return Page();
            }

            _context.Inscrieri.Add(Inscriere);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

