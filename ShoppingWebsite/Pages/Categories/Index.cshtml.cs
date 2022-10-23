using Microsoft.EntityFrameworkCore;
using ShoppingWebsite.Data;
using ShoppingWebsite.Model;
using ShoppingWebsite.Services.AuthorizeFilter;
using ShoppingWebsite.Services.ManageState;

namespace ShoppingWebsite.Pages.Categories;

[AuthorizeFilter("Admin")]
public class IndexModel : StateModel
{

    public IndexModel(ApplicationDbContext db) : base(db)
    {
    }

    public IList<Category> Categories { get;set; } = default!;

    public async Task OnGetAsync()
    {
        if (_db.Categories != null)
        {
            Categories = await _db.Categories.ToListAsync();
        }
    }
}
