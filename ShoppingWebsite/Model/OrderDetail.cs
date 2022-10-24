using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ShoppingWebsite.Model;

[Table("OrderDetail")]
[Index(nameof(OrderId), nameof(ProductId), IsUnique = true, Name = "Ix_OrderDetail")]
public class OrderDetail
{

    [ForeignKey("Order")]
    public int OrderId { get; set; }

    [ForeignKey("Product")]
    public int ProductId { get; set; }

    public double Price { get; set; }
    public int Quantity { get; set; }

    // Relationship
    public Order Order { get; set; }

    public Product Product { get; set; }

}