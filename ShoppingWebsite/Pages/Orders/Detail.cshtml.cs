using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingWebsite.Data;
using ShoppingWebsite.Model;
using ShoppingWebsite.Model.Enum;
using ShoppingWebsite.Services.ManageState;
using System.ComponentModel.DataAnnotations;

namespace ShoppingWebsite.Pages.Orders;

public class DetailModel : StateModel
{
    public DetailModel(ApplicationDbContext db) : base(db)
    {
    }
    public Order Order { get; set; }
    public List<OrderDetail> OrderDetails { get; set; }

    public IActionResult OnGet(int id)
    {
        if (!IsAuthenticated)
        {
            return Redirect("/Login/Index");
        }

        Order = _db.Orders
                    .Include(order => order.Customer)
                    .SingleOrDefault(order => order.OrderId.Equals(id));

        if (Order == null)
        {
            return NotFound();
        }

        // check forbidden
        if (!Account.Type.Equals(AccountType.Staff) && !Account.AccountID.Equals(Order.CustomerId))
        {
            return StatusCode(StatusCodes.Status403Forbidden);
        }

        OrderDetails = _db.OrderDetails
            .Include(orderDetail => orderDetail.Product)
            .Include(orderDetail => orderDetail.Product.Category)
            .Include(orderDetail => orderDetail.Product.Supplier)
            .Where(od => od.OrderId.Equals(Order.OrderId)).ToList();

        return Page();
    }
}
