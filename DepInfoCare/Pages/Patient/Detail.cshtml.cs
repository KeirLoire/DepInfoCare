using DepInfoCare.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DepInfoCare.Pages.Patient
{
    public class DetailModel : PageModelBase
    {
        public FacilityModel Facility { get; set; }
        public PatientModel Patient { get; set; }

        public async Task<IActionResult> OnGetAsync([FromRoute] int facilityId, [FromRoute] int patientId)
        {
            Facility = await DepContext
                .Facilities
                .FirstOrDefaultAsync(x => x.Id == facilityId);

            if (Facility == null)
                return NotFound();

            Patient = await DepContext
                .Patients
                .FirstOrDefaultAsync(x => x.Id == patientId);

            if (Patient == null)
                return NotFound();

            Breadcrumb = new Breadcrumb
            {
                Title = Patient.FullName,
                Items = new()
                {
                    new BreadcrumbItem
                    {
                        Title = "Facilities",
                        Url = "/facility"
                    },
                    new BreadcrumbItem
                    {
                        Title = Facility.Name,
                        Url = $"/facility/{Facility.Id}"
                    }
                }
            };

            return Page();
        }
    }
}
