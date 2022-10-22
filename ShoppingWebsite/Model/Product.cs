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
    public int CategoryId { get; set; }

    [ForeignKey("Supplier")]
    public int SupplierId { get; set; }

    public string ProductDescription { get; set; }

    [Required]
    public int QuantityPerUnit { get; set; }

    [Required]
    public double UnitPrice { get; set; }

    public string ProductImageURL { get; set; }

    // Relationship
    public Category Category { get; set; }

    public Supplier Supplier { get; set; }


}
