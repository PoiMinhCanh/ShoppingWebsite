using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingWebsite.Data;
using ShoppingWebsite.Model;
using ShoppingWebsite.Model.Enum;

namespace ShoppingWebsite.Services.ManageState;

public class StateModel : PageModel
{
    protected readonly ApplicationDbContext _db;

    public Account Account { get; private set; }
    public bool IsAuthenticated { get; private set; }

    public List<string> SuccessMessages = new List<string>();
    public List<string> ErrorMessages = new List<string>();

    public StateModel(ApplicationDbContext db)
    {
        _db = db;
    }

    // Run code after a handler method has been selected, but before model binding occurs.
    public override void OnPageHandlerSelected(PageHandlerSelectedContext context)
    {
        getAccount();
    }

    private void getAccount()
    {
        var result = HttpContext.Request.Cookies.TryGetValue("id", out string id);
        
        // not have id in cookie
        if (id == null)
        {
            Account = null;
            IsAuthenticated = false;
            return;
        } 
        // get account
        Account = _db.Accounts.SingleOrDefault(account => account.AccountID.ToString().Equals(id));

        if (Account == null)
        {
            IsAuthenticated = false;
            return;
        }

        IsAuthenticated = true;

    }

    public bool IsAdmin()
    {
        return IsAuthenticated && Account.Type.Equals(AccountType.Staff);
    }

}

