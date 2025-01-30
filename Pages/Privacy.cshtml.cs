using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AplicatieStudenti.Pages
{
    public class PrivacyModel(ILogger<PrivacyModel> logger) : PageModel
    {
#pragma warning disable IDE0052 // Remove unread private members
        private readonly ILogger<PrivacyModel> _logger = logger;
#pragma warning restore IDE0052 // Remove unread private members

        public void OnGet()
        {
        }
    }
}