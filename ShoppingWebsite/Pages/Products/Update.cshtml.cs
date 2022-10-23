using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoppingWebsite.Data;
using ShoppingWebsite.Model;
using ShoppingWebsite.Services.AuthorizeFilter;
using ShoppingWebsite.Services.FileProcessor;
using ShoppingWebsite.Services.ManageState;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ShoppingWebsite.Pages.Products;

[AuthorizeFilter("Admin")]
public class UpdateModel : StateModel
{

    public UpdateModel(ApplicationDbContext db) : base(db)
    {
    }

    [BindProperty]
    public Product Product { get; set; }

    [DataType(DataType.Upload)]
    //[FileExtensions(Extensions = "png,jpg,jpeg,gif")] 
    // file .png bi bao loi
    [Display(Name = "Choose Product Image")]
    [BindProperty]
    public IFormFile ProductImageFile { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null || _db.Products == null)
        {
            return NotFound();
        }

        var product =  await _db.Products.FirstOrDefaultAsync(m => m.ProductId == id);
        if (product == null)
        {
            return NotFound();
        }
        Product = product;

        ViewData["Categories"] = new SelectList(_db.Categories, "CategoryId", "CategoryName");
        ViewData["Suppliers"] = new SelectList(_db.Suppliers, "SupplierId", "CompanyName");

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

        if (!ProductExists(Product.ProductId))
        {
            return NotFound();
        }

        if (ProductImageFile != null)
        {
            string fileName = $"Product_{Product.ProductId}";

            string saveFilePath = ImageProcessor.SaveImage(ProductImageFile, fileName).GetAwaiter().GetResult();

            Product.ProductImageURL = saveFilePath;
        }

        _db.Products.Update(Product);
        await _db.SaveChangesAsync();

        return RedirectToPage("./Index");
    }

    private bool ProductExists(int id)
    {
      return _db.Products.Any(e => e.ProductId == id);
    }
}