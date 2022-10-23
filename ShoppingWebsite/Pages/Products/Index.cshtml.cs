using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingWebsite.Data;
using ShoppingWebsite.Model;
using ShoppingWebsite.Services.AuthorizeFilter;
using ShoppingWebsite.Services.FileProcessor;
using ShoppingWebsite.Services.ManageState;

namespace ShoppingWebsite.Pages.Products;

[AuthorizeFilter("Admin")]
public class IndexModel : StateModel
{

    public IndexModel(ApplicationDbContext db) : base(db)
    {
    }

    public IList<Product> Products { get;set; } = default!;

    public async Task OnGetAsync()
    {
        if (_db.Products != null)
        {
            Products = await _db.Products
            .Include(p => p.Category)
            .Include(p => p.Supplier).ToListAsync();
        }
    }

    public IActionResult OnGetDelete(int id)
    {
        Product product = _db.Products.SingleOrDefault(product => product.ProductId.Equals(id));
        
        if (product == null)
        {
            return NotFound();
        }

        ImageProcessor.deleteFileIfExist(product.ProductImageURL);

        _db.Products.Remove(product);
        _db.SaveChanges();

        return Redirect("Products/Index");

    }
}
