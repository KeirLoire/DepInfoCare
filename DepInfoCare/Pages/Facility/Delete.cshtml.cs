using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DepInfoCare.Pages.Facility
{
    [Authorize(Roles = "Administrator")]
    public class DeleteModel : PageModelBase
    {
        public async Task<IActionResult> OnGetAsync([FromRoute] int id)
        {
            var facility = await DepContext.Facilities.FirstOrDefaultAsync(x => x.Id == id);

            if (facility == null)
                return NotFound();

            DepContext.Facilities.Remove(facility);

            await DepContext.SaveChangesAsync();

            return Redirect("/facility");
        }
    }
}
