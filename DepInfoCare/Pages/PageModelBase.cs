using DepInfoCare.Data;
using DepInfoCare.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DepInfoCare.Pages
{
    public abstract class PageModelBase : PageModel
    {
        protected DepInfoCareDbContext DepContext => HttpContext.RequestServices.GetRequiredService<DepInfoCareDbContext>();
    
        public Breadcrumb Breadcrumb;
    }
}
