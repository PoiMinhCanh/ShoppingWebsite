using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingWebsite.Data;
using ShoppingWebsite.Model;
using ShoppingWebsite.Services.AuthorizeFilter;
using ShoppingWebsite.Services.FileProcessor;
using ShoppingWebsite.Services.ManageState;
using System.ComponentModel.DataAnnotations;

namespace ShoppingWebsite.Pages.Products;

[AuthorizeFilter("Admin")]
public class CreateModel : StateModel
{

    public CreateModel(ApplicationDbContext db) : base(db)
    {
    }

    [Required(ErrorMessage = "Please choose at least one file.")]
    [DataType(DataType.Upload)]
    //[FileExtensions(Extensions = "png,jpg,jpeg,gif")] 
    // file .png bi bao loi
    [Display(Name = "Choose Product Image")]
    [BindProperty]
    public IFormFile ProductImageFile { get; set; }

    public IActionResult OnGet()
    {
        ViewData["Categories"] = new SelectList(_db.Categories, "CategoryId", "CategoryName");
        ViewData["Suppliers"] = new SelectList(_db.Suppliers, "SupplierId", "CompanyName");
        return Page();
    }

    [BindProperty]
    public Product Product { get; set; }

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        // Add product
        _db.Products.Add(Product);
        await _db.SaveChangesAsync();

        // save image and update product
        string fileName = $"Product_{Product.ProductId}";
        string saveFilePath = ImageProcessor.SaveImage(ProductImageFile, fileName).GetAwaiter().GetResult();
        Product.ProductImageURL = saveFilePath;

        _db.Products.Update(Product);
        await _db.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
