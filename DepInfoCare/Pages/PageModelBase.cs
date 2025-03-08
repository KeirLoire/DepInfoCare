using DepInfoCare.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DepInfoCare.Pages
{
    public class PageModelBase : PageModel
    {
        protected DepInfoCareDbContext DepContext => HttpContext.RequestServices.GetRequiredService<DepInfoCareDbContext>();
    }
}
