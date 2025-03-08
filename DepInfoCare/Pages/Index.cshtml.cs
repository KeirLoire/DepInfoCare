using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DepInfoCare.Pages
{
    [Authorize]
    public class IndexModel : PageModelBase
    {
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}
