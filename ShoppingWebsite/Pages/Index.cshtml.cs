using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingWebsite.Data;
using ShoppingWebsite.Helper;
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

    public int cartSize { get; set; } = 0;

    public IActionResult OnGet(string q, int? categoryId, int? addProductId)
    {
        // if authenticated and add product to card
        if (IsAuthenticated)
        {
            // get cart, to calculate cart size
            Dictionary<int, int> cart;
            // get cart from session
            string rawValueCart = HttpContext.Session.GetString("cart");

            if (rawValueCart == null)
            {
                // cart not exist
                // create new card
                cart = new Dictionary<int, int>();
                cartSize = 0;
            } else
            {
                cart = CastObject.Deserialize<Dictionary<int, int>>(rawValueCart);
                cartSize = cart.Sum(x => x.Value);
            }

            if (addProductId != null)
            {
                Product product = _db.Products.SingleOrDefault(product => product.ProductId.Equals(addProductId));
                if (product == null)
                {
                    return NotFound();
                }

                // get value
                cart.TryGetValue(product.ProductId, out var currentCount);
                cart[product.ProductId] = currentCount + 1;

                // add card to session
                HttpContext.Session.SetString("cart", CastObject.Serialize(cart));

                return RedirectToPage("./Index", new { q, categoryId });
            }
        }

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
        
        return Page();
    }

}
