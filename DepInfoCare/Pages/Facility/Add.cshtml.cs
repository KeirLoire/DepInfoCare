using DepInfoCare.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DepInfoCare.Pages.Facility
{
    [Authorize(Roles = "Administrator")]
    public class AddModel : PageModelBase
    {
        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string name, string address)
        {
            if (DepContext.Facilities.Any(f => f.Name == name))
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

            return RedirectToPage("/Facility/Index");
        }
    }
}
