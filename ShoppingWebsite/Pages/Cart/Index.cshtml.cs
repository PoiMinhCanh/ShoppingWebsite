using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingWebsite.Data;
using ShoppingWebsite.Helper;
using ShoppingWebsite.Model;
using ShoppingWebsite.Services.ManageState;

namespace ShoppingWebsite.Pages.Cart;

public class IndexModel : StateModel
{
    public IndexModel(ApplicationDbContext db) : base(db)
    {
    }

    public List<Product> Products { get; set; } = new List<Product>();
    public int cartSize { get; set; } = 0;
    public double TotalCost { get; set; } = 0;

    public IActionResult OnGet()
    {
        if (!IsAuthenticated)
        {
            return Redirect("/Login/Index");
        }
        
        // get cart, to calculate cart size
        Dictionary<int, int> cart = getCart();

        // get product items
        getProductItems(cart);

        return Page();
    }

    public IActionResult OnGetOrder()
    {
        if (!IsAuthenticated)
        {
            return Redirect("/Login/Index");
        }

        // get cart, to calculate cart size
        Dictionary<int, int> cart = getCart();

        if (cartSize == 0)
        {
            ErrorMessages.Add("No have any data.");
            return Page();
        }
        // Create Order and OrderDetail into database, redirect to detail order
        Customer profile = _db.Customers.SingleOrDefault(profile => profile.CustomerID.Equals(Account.AccountID));

        // get product items
        getProductItems(cart);
        
        Order order = new Order
        {
            CustomerId = profile.CustomerID,
            Freight = 20,
            ShipAddress = profile.Address,
            TotalCost = TotalCost + 20,
        };

        order.OrderDetails = new List<OrderDetail>();

        Products.ForEach(product =>
        {
            order.OrderDetails.Add(
                new OrderDetail
                {
                    ProductId = product.ProductId,
                    Price = product.Price * product.Quantity,
                    Quantity = product.Quantity,
                }
            );
        });

        _db.Orders.Add(order);
        _db.SaveChanges();

        // remove cart
        HttpContext.Session.Remove("cart");

        return Redirect($"/Orders/Detail?id={order.OrderId}");
    }   

    public Dictionary<int, int> getCart()
    {
        Dictionary<int, int> cart;
        // get cart from session
        string rawValueCart = HttpContext.Session.GetString("cart");

        if (rawValueCart == null)
        {
            // cart not exist
            // create new card
            cart = new Dictionary<int, int>();
            cartSize = 0;
        }
        else
        {
            cart = CastObject.Deserialize<Dictionary<int, int>>(rawValueCart);
            cartSize = cart.Sum(x => x.Value);
        }
        return cart;
    }

    public void getProductItems(Dictionary<int, int> cart)
    {
        foreach (var item in cart)
        {
            Product product = _db.Products
                                .Include(product => product.Category)
                                .Include(product => product.Supplier)
                                .SingleOrDefault(product => product.ProductId.Equals(item.Key));
            if (product != null)
            {
                product.Quantity = item.Value;
                Products.Add(product);
                TotalCost += product.Price * product.Quantity;
            }
        }
    }
}
