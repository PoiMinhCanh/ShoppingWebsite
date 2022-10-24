using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingWebsite.Data;
using ShoppingWebsite.Model;
using ShoppingWebsite.Model.Enum;
using ShoppingWebsite.Services.ManageState;

namespace ShoppingWebsite.Pages.Orders;

public class HistoryModel : StateModel
{
    public HistoryModel(ApplicationDbContext db) : base(db)
    {
    }
    public List<Order> Orders { get; set; }

    public IActionResult OnGet(int customerId)
    {
        if (!IsAuthenticated)
        {
            return Redirect("/Login/Index");
        }

        if (!Account.Type.Equals(AccountType.Staff) && !Account.AccountID.Equals(customerId))
        {
            return StatusCode(StatusCodes.Status403Forbidden);
        }

        // get orders of user
        Orders = _db.Orders.Where(order => order.CustomerId.Equals(customerId)).ToList();

        return Page();
    }
}
