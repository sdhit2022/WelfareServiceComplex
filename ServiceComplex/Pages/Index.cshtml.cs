using Application.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceComplex.Pages;

public class IndexModel : PageModel
{
    private readonly IAuthHelper _authHelper;

    public IndexModel(IAuthHelper authHelper)
    {
        _authHelper = authHelper;
    }

    public void OnGet()
    {
    }
}