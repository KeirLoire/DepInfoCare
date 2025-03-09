using DepInfoCare.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;

namespace DepInfoCare.Pages.Patient
{
    [Authorize(Roles = "Administrator")]
    public class AddModel : PageModelBase
    {
        [BindProperty]
        public PatientModel FormData { get; set; }
        public FacilityModel Facility { get; set; }


        public async Task<IActionResult> OnGetAsync([FromRoute] int facilityId, [FromRoute] int patientId = 0)
        {
            Facility = await DepContext
                .Facilities
                .FirstOrDefaultAsync(x => x.Id == facilityId);

            if (Facility == null)
                return NotFound();

            if (patientId != 0)
            {
                FormData = await DepContext
                    .Patients
                    .FirstOrDefaultAsync(x => x.Id == patientId);
            }
            else
            {
                FormData = new();
            }

            if (FormData == null)
                return NotFound();

            Breadcrumb = new Breadcrumb
            {
                Title = !string.IsNullOrWhiteSpace(FormData.FirstName)
                    ? "Edit Patient"
                    : "Add Patient",
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

        public async Task<IActionResult> OnPostAsync([FromRoute] int facilityId, [FromRoute] int patientId = 0)
        {
            Facility = await DepContext
                .Facilities
                .FirstOrDefaultAsync(x => x.Id == facilityId);

            if (Facility == null)
                return NotFound();

            if (!ModelState.IsValid)
                return Page();

            FormData.FacilityId = Facility.Id;

            if (FormData.ImageFile != null)
            {
                using var memoryStream = new MemoryStream();
                await FormData.ImageFile.CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                using var image = Image.Load(memoryStream);
                image.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(64, 64),
                    Mode = ResizeMode.Max
                }));

                image.Save(memoryStream, new PngEncoder());
                var base64 = Convert.ToBase64String(memoryStream.ToArray());

                FormData.ImageUrl = $"data:{FormData.ImageFile.ContentType};base64,{base64}";
            }

            if (patientId != 0)
            {
                var patient = await DepContext
                    .Patients
                    .FirstOrDefaultAsync(x => x.Id == patientId);

                if (patient == null)
                    return NotFound();

                patient.FirstName = FormData.FirstName;
                patient.MiddleName = FormData.MiddleName;
                patient.LastName = FormData.LastName;
                patient.Suffix = FormData.Suffix;
                patient.Room = FormData.Room;
                patient.UpdatedAt = DateTime.Now;

                if (!string.IsNullOrWhiteSpace(FormData.ImageUrl))
                    patient.ImageUrl = FormData.ImageUrl;

                DepContext.Patients.Update(patient);
            }
            else
            {
                DepContext.Patients.Add(FormData);
            }

            await DepContext.SaveChangesAsync();

            return Redirect($"/facility/{Facility.Id}");
        }
    }
}
