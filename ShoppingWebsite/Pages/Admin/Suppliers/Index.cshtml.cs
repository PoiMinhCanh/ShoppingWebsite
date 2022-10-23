using Microsoft.EntityFrameworkCore;
using ShoppingWebsite.Data;
using ShoppingWebsite.Model;
using ShoppingWebsite.Services.AuthorizeFilter;
using ShoppingWebsite.Services.ManageState;

namespace ShoppingWebsite.Pages.Admin.Suppliers;

[AuthorizeFilter("Admin")]
public class IndexModel : StateModel
{
    public IndexModel(ApplicationDbContext db) : base(db)
    {
    }

    public IList<Supplier> Suppliers { get;set; }

    public async Task OnGetAsync()
    {
        if (_db.Suppliers != null)
        {
            Suppliers = await _db.Suppliers.ToListAsync();
        }
    }
}
