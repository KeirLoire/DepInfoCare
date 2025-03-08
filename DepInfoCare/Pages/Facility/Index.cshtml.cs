using DepInfoCare.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DepInfoCare.Pages.Facility
{
    [Authorize]
    public class IndexModel : PageModelBase
    {
        public List<FacilityModel> Facilities { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Facilities = await DepContext.Facilities.ToListAsync();

            return Page();
        }
    }
}
