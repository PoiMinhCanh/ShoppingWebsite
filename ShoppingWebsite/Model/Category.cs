using ShoppingWebsite.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShoppingWebsite.Model;

[Table("Category")]
public class Category
{
    [Key]
    public int CategoryId { get; set; }

    [Required]
    [StringLength(200, MinimumLength = 1)]
    public string CategoryName { get; set; }

    [Required]
    [StringLength(200, MinimumLength = 1)]
    public string Description { get; set; }

    // Relationship
    public ICollection<Product> Products { get; set; }

}
