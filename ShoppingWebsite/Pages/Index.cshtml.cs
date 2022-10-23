using Microsoft.EntityFrameworkCore;
using ShoppingWebsite.Data;
using ShoppingWebsite.Model;
using ShoppingWebsite.Services.ManageState;

namespace ShoppingWebsite.Pages;

public class IndexModel : StateModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ApplicationDbContext db, ILogger<IndexModel> logger) : base(db)
    {
        _logger = logger;
    }

    public List<Category> Categories { get; set; }
    public List<Product> Products { get; set; }

    public int? ActiveCategoryId { get; set; } = null;

    public string TextSearch { get; set; }

    public void OnGet(string q, int? categoryId)
    {
        TextSearch = q ?? "";
        ActiveCategoryId = categoryId ?? null;

        // get list category
        Categories = _db.Categories.ToList();

        // get lish product based on categoryId/ActiveCategoryId
        // and search text
        Products = _db.Products
           .Include(p => p.Category)
           .Include(p => p.Supplier)
           .Where(product => (ActiveCategoryId == null ? true : product.CategoryId == ActiveCategoryId)
                          && (product.ProductName.ToLower().Contains(TextSearch.ToLower())
                              || product.ProductDescription.ToLower().Contains(TextSearch.ToLower())
                              || product.Category.CategoryName.ToLower().Contains(TextSearch.ToLower())
                              || product.Supplier.CompanyName.ToLower().Contains(TextSearch.ToLower()))).ToList();

    }

}
