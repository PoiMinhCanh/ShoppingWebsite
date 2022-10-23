using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingWebsite.Data;
using ShoppingWebsite.Model;
using ShoppingWebsite.Services.AuthorizeFilter;
using ShoppingWebsite.Services.ManageState;

namespace ShoppingWebsite.Pages.Suppliers;

[AuthorizeFilter("Admin")]
public class DeleteModel : StateModel
{
    public DeleteModel(ApplicationDbContext db) : base(db)
    {
    }

    [BindProperty]
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

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null || _db.Suppliers == null)
        {
            return NotFound();
        }
        var supplier = await _db.Suppliers.FindAsync(id);

        if (supplier != null)
        {
            Supplier = supplier;
            _db.Suppliers.Remove(Supplier);
            await _db.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}