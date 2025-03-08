using DepInfoCare.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DepInfoCare.Pages.Facility
{
    [Authorize(Roles = "Administrator")]
    public class AddModel : PageModelBase
    {
        public FacilityModel Facility { get; set; }

        public async Task<IActionResult> OnGetAsync([FromRoute] int id = 0)
        {
            if (id != 0)
            {
                Facility = await DepContext.Facilities.FirstOrDefaultAsync(x => x.Id == id);

                if (Facility == null)
                    return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string name, string address, [FromRoute] int id = 0)
        {
            if (id != 0)
            {
                Facility = await DepContext.Facilities.FirstOrDefaultAsync(x => x.Id == id);

                if (Facility == null)
                    return NotFound();

                if (DepContext.Facilities.Any(x => x.Name == name && x.Id != id))
                {
                    ViewData["Message"] = "Facility already exists.";
                    return Page();
                }

                Facility.Name = name;
                Facility.Address = address;

                DepContext.Facilities.Update(Facility);

                await DepContext.SaveChangesAsync();
            }
            else
            {
                if (DepContext.Facilities.Any(x => x.Name == name))
                {
                    ViewData["Message"] = "Facility already exists.";
                    return Page();
                }

                var facility = new FacilityModel
                {
                    Name = name,
                    Address = address
                };

                DepContext.Facilities.Add(facility);

                await DepContext.SaveChangesAsync();
            }

            return Redirect("/facility");
        }
    }
}
