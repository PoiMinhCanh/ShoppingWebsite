using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingWebsite.Data;
using ShoppingWebsite.Model;
using ShoppingWebsite.Services.AuthorizeFilter;
using ShoppingWebsite.Services.ManageState;

namespace ShoppingWebsite.Pages.Categories;

[AuthorizeFilter("Admin")]
public class DeleteModel : StateModel
{

    public DeleteModel(ApplicationDbContext db) : base(db)
    {
    }

    [BindProperty]
    public Category Category { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null || _db.Categories == null)
        {
            return NotFound();
        }

        var category = await _db.Categories.FirstOrDefaultAsync(m => m.CategoryId == id);

        if (category == null)
        {
            return NotFound();
        }
        else 
        {
            Category = category;
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null || _db.Categories == null)
        {
            return NotFound();
        }
        var category = await _db.Categories.FindAsync(id);

        if (category != null)
        {
            Category = category;
            _db.Categories.Remove(Category);
            await _db.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
