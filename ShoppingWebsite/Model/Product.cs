using ShoppingWebsite.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShoppingWebsite.Model;

[Table("Product")]
public class Product
{
    [Key]
    public int ProductId { get; set; }

    [Required]
    [StringLength(200, MinimumLength = 1)]
    public string ProductName { get; set; }

    [ForeignKey("Category")]
    [Display(Name = "Category")]
    public int CategoryId { get; set; }

    [ForeignKey("Supplier")]
    [Display(Name = "Supplier")]
    public int SupplierId { get; set; }

    [Required]
    [StringLength(200, MinimumLength = 1)]
    public string ProductDescription { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than or equal {1}")]
    public int Quantity { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than or equal {1}")]
    public double Price { get; set; }

    [Display(Name = "Product Image")]
    public string ProductImageURL { get; set; }

    // Relationship
    public Category Category { get; set; }

    public Supplier Supplier { get; set; }


}
