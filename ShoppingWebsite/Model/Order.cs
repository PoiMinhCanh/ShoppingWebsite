using ShoppingWebsite.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShoppingWebsite.Model;

[Table("Order")]
public class Order
{
    [Key]
    public int OrderId { get; set; }

    [ForeignKey("Customer")]
    public int CustomerId { get; set; } // fk
    // this property reference CustomerId of Customer 

    public DateTime OrderDate { get; set; } = DateTime.Now;

    public DateTime RequiredDate { get; set; } = DateTime.Now + TimeSpan.FromDays(7);

    public DateTime ShippedDate { get; set; } = DateTime.Now + TimeSpan.FromDays(3);

    // tien ship
    public double Freight { get; set; } = 25000; 

    public string ShipAddress { get; set; }
    public double TotalCost { get; set; }

    // Relationship
    public Customer Customer { get; set; }
    // with pk is CustomerID

    public ICollection<OrderDetail> OrderDetails { get; set; }
}