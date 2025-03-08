using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DepInfoCare.Pages.Patient
{
    [Authorize(Roles = "Administrator")]
    public class DeleteModel : PageModelBase
    {
        public async Task<IActionResult> OnGetAsync([FromRoute] int facilityId, [FromRoute] int patientId)
        {
            var facility = await DepContext.Facilities.FirstOrDefaultAsync(x => x.Id == facilityId);

            if (facility == null)
                return NotFound();

            var patient = await DepContext.Patients.FirstOrDefaultAsync(x => x.Id == patientId);

            DepContext.Patients.Remove(patient);

            await DepContext.SaveChangesAsync();

            return Redirect($"/facility/{facilityId}");
        }
    }
}
