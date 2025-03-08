using DepInfoCare.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DepInfoCare.Pages.Patient
{
    [Authorize]
    public class IndexModel : PageModelBase
    {
        public FacilityModel Facility { get; set; }
        public List<PatientModel> Patients { get; set; }

        public async Task<IActionResult> OnGetAsync([FromRoute] int facilityId)
        {
            Facility = await DepContext
                .Facilities
                .FirstOrDefaultAsync(x => x.Id == facilityId);

            if (Facility == null)
                return NotFound();

            Patients = await DepContext
                .Patients
                .Where(x => x.FacilityId == Facility.Id)
                .ToListAsync();

            return Page();
        }
    }
}
