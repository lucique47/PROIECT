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

        public List<SelectListItem> CursList { get; set; } = [];
        public List<SelectListItem> ProfesorList { get; set; } = [];

        public async Task<IActionResult> OnGetAsync()
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

