using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShoppingWebsite.Model;

[Table("Customer")]
public class Customer
{
    // Foreign key for Standard
    [Key]
    [ForeignKey("Account")]
    public int CustomerID { get; set; } // this property reference AccountID of account 

    [Required]
    [StringLength(50, MinimumLength = 1)]
    public string ContactName { get; set; }

    [Required]
    [StringLength(200, MinimumLength = 1)]
    public string Address { get; set; }

    [Required]
    [Phone]
    public string Phone { get; set; }

    // Relationship
    public Account Account { get; set; } // with pk is CustomerID

    public ICollection<Order> Orders { get; set; }

}
