using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using ShoppingWebsite.Data;
using ShoppingWebsite.Model.DTO;
using ShoppingWebsite.Model.Enum;
using ShoppingWebsite.Model;
using ShoppingWebsite.Services.ManageState;

namespace ShoppingWebsite.Pages.Login;

public class IndexModel : StateModel
{
    private readonly IMapper _mapper;

    [BindProperty]
    public LoginDTO LoginDTO { get; set; }

    public IndexModel(ApplicationDbContext db, IMapper mapper) : base(db)
    {
        _mapper = mapper;
    }

    public IActionResult OnGet()
    {
        if (IsAuthenticated)
        {
            return Redirect("/");
        }
        return Page();
    }

    public IActionResult OnPost()
    {
        if (ModelState.IsValid)
        {
            Account account = _db.Accounts.SingleOrDefault(account => account.UserName.Equals(LoginDTO.UserName)
                                                                   && account.Password.Equals(LoginDTO.Password));    

            if (account == null)
            {
                ErrorMessages.Add("Username or password not correct. Please try again!");
                return Page();
            }

            // set value on cookies
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = LoginDTO.RememberMe ? DateTime.Now.AddDays(30) : null
            };

            HttpContext.Response.Cookies.Append("id", account.AccountID.ToString(), cookieOptions);
            HttpContext.Response.Cookies.Append("role", account.Type == AccountType.Staff ? "Admin" : "Member", cookieOptions);

            // check if already create profile or not
            Customer profile = _db.Customers.SingleOrDefault(profile => profile.CustomerID.Equals(account.AccountID));
            if (profile == null)
            {
                profile = new Customer 
                            { CustomerID = account.AccountID, 
                              ContactName = "Poi Minh Canh", 
                              Address = "Bac Lieu",
                              Phone = "0857817812"};
                _db.Add(profile);
                _db.SaveChanges();

                return Redirect($"Profile/Index?id={account.AccountID}");
            }

            return Redirect("Index");

        }
        return Page();
    }
}


