using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShoppingWebsite.Model;

[Table("Supplier")]
public class Supplier
{
    [Key]
    public int SupplierId { get; set; }

    [Required]
    [StringLength(200, MinimumLength = 1)]
    public string CompanyName { get; set; }
    
    [Required]
    [StringLength(200, MinimumLength = 1)]
    public string Address { get; set; }

    [Required]
    [Phone]
    public string Phone { get; set; }

    // Relationship
    public ICollection<Product> Products { get; set; }
}