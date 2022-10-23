using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingWebsite.Data;
using ShoppingWebsite.Model;
using ShoppingWebsite.Services.AuthorizeFilter;
using ShoppingWebsite.Services.ManageState;

namespace ShoppingWebsite.Pages.Categories;

[AuthorizeFilter("Admin")]
public class DetailModel : StateModel
{

    public DetailModel(ApplicationDbContext db) : base(db)
    {
    }

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
}
