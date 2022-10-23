using Microsoft.AspNetCore.Mvc;
using ShoppingWebsite.Data;
using ShoppingWebsite.Model;
using ShoppingWebsite.Services.AuthorizeFilter;
using ShoppingWebsite.Services.ManageState;

namespace ShoppingWebsite.Pages.Suppliers;

[AuthorizeFilter("Admin")]
public class CreateModel : StateModel
{

    public CreateModel(ApplicationDbContext db) : base(db)
    {
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public Supplier Supplier { get; set; }
        
    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _db.Suppliers.Add(Supplier);
        await _db.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}