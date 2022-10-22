using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ShoppingWebsite.Pages.Logout;

public class IndexModel : PageModel
{
    public IActionResult OnGet()
    {
        HttpContext.Response.Cookies.Delete("id");
        return Redirect("/Index");
    }
}
