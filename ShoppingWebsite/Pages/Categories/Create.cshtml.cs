using Microsoft.AspNetCore.Mvc;
using ShoppingWebsite.Data;
using ShoppingWebsite.Model;
using ShoppingWebsite.Services.AuthorizeFilter;
using ShoppingWebsite.Services.ManageState;

namespace ShoppingWebsite.Pages.Categories;

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
    public Category Category { get; set; }
    

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
      if (!ModelState.IsValid)
        {
            return Page();
        }

        _db.Categories.Add(Category);
        await _db.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
