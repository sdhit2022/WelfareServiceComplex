using infrastructure.Attribute;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceComplex.Pages;

public class ErrorModel : PageModel
{
    [IgnoreFilter]
    public IActionResult OnGetServer(string value)
    {
        return Partial("PartialView/Server", value);
    }
}