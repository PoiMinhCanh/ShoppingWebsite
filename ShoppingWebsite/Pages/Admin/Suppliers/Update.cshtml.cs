using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShoppingWebsite.Data;
using ShoppingWebsite.Model;
using ShoppingWebsite.Services.AuthorizeFilter;
using ShoppingWebsite.Services.ManageState;

namespace ShoppingWebsite.Pages.Admin.Suppliers;

[AuthorizeFilter("Admin")]
public class UpdateModel : StateModel
{

    public UpdateModel(ApplicationDbContext db) : base(db)
    {
    }

    [BindProperty]
    public Supplier Supplier { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null || _db.Suppliers == null)
        {
            return NotFound();
        }

        var supplier =  await _db.Suppliers.FirstOrDefaultAsync(m => m.SupplierId == id);
        if (supplier == null)
        {
            return NotFound();
        }
        Supplier = supplier;
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _db.Attach(Supplier).State = EntityState.Modified;

        try
        {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SupplierExists(Supplier.SupplierId))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Index");
    }

    private bool SupplierExists(int id)
    {
        return _db.Suppliers.Any(e => e.SupplierId == id);
    }
}

