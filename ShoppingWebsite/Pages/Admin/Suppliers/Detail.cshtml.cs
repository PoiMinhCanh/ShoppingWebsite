using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingWebsite.Data;
using ShoppingWebsite.Model;
using ShoppingWebsite.Services.AuthorizeFilter;
using ShoppingWebsite.Services.ManageState;

namespace ShoppingWebsite.Pages.Admin.Suppliers;

[AuthorizeFilter("Admin")]
public class DetailModel : StateModel
{

    public DetailModel(ApplicationDbContext db) : base(db)
    {
    }

    public Supplier Supplier { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null || _db.Suppliers == null)
        {
            return NotFound();
        }

        var supplier = await _db.Suppliers.FirstOrDefaultAsync(m => m.SupplierId == id);
        if (supplier == null)
        {
            return NotFound();
        }
        else 
        {
            Supplier = supplier;
        }
        return Page();
    }
}
