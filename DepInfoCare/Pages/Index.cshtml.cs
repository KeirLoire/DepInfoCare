using Microsoft.AspNetCore.Mvc;

namespace DepInfoCare.Pages
{
    public class IndexModel : PageModelBase
    {
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}
