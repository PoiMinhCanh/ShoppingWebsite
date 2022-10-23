using ShoppingWebsite.Data;
using ShoppingWebsite.Services.AuthorizeFilter;
using ShoppingWebsite.Services.ManageState;

namespace ShoppingWebsite.Pages.Admin;

[AuthorizeFilter("Admin")]
public class IndexModel : StateModel
{
    public IndexModel(ApplicationDbContext db) : base(db)
    {
    }

    public void OnGet()
    {
    }
}
